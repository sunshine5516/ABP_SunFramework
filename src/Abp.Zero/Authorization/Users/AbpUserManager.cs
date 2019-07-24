using Abp.Zero.Authorization.Roles;
using Microsoft.AspNet.Identity;
using AbpFramework.Domain.Services;
using AbpFramework.Runtime.Session;
using AbpFramework.Runtime.Caching;
using AbpFramework.Configuration;
using AbpFramework.Domain.Uow;
using System;
using Abp.Zero.Common.Zero.Configuration;
using System.Threading.Tasks;
using AbpFramework.Authorization;
using Abp.Zero.Common.Authorization.Users;
using Abp.Zero.Common.Runtime.Caching;
using AbpFramework;
using System.Linq;

namespace Abp.Zero.Authorization.Users
{
    /// <summary>
    /// 扩展ASP.NET Identity Framework的 <see cref="UserManager{TUser,TKey}"/>方法
    /// </summary>
    /// <typeparam name="TRole"></typeparam>
    /// <typeparam name="TUser"></typeparam>
    public abstract class AbpUserManager<TRole, TUser>
        : UserManager<TUser, long>, IDomainService
        where TRole : AbpRole<TUser>, new()
        where TUser : AbpUser<TUser>
    {
        #region 声明实例
        protected IUserPermissionStore<TUser> UserPermissionStore
        {
            get
            {
                if (!(Store is IUserPermissionStore<TUser>))
                {
                    throw new AbpException("Store is not IUserPermissionStore");
                }

                return Store as IUserPermissionStore<TUser>;
            }
        }
        public IAbpSession AbpSession { get; set; }
        public AbpUserStore<TRole, TUser> AbpStore { get; }
        //private AbpRoleManager<TRole,TUser> RoleManager { get; }
        private readonly ICacheManager _cacheManager;
        private readonly ISettingManager _settingManager;
        private readonly IPermissionManager _permissionManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        protected AbpRoleManager<TRole, TUser> RoleManager { get; }
        //private readonly IOrganizationUnitSettings _organizationUnitSettings;
        #endregion
        #region 构造函数
        public AbpUserManager(AbpUserStore<TRole, TUser> userStore,
           IUnitOfWorkManager unitOfWorkManager,
           AbpRoleManager<TRole, TUser> roleManager,
            ICacheManager cacheManager, ISettingManager settingManager,
            IPermissionManager permissionManager)
            : base(userStore)
        {
            AbpStore = userStore;
            RoleManager = roleManager;
            _cacheManager = cacheManager;
            _settingManager = settingManager;
            _unitOfWorkManager = unitOfWorkManager;
            this._permissionManager = permissionManager;
        }
        #endregion
        #region 方法
        public virtual void InitializeLockoutSettings(int? tenantId)
        {
            UserLockoutEnabledByDefault = IsTrue(AbpZeroSettingNames.UserManagement.UserLockOut.IsEnabled, tenantId);
            DefaultAccountLockoutTimeSpan = TimeSpan.FromSeconds(GetSettingValue<int>
                (AbpZeroSettingNames.UserManagement.UserLockOut.DefaultAccountLockoutSeconds, tenantId));
            MaxFailedAccessAttemptsBeforeLockout = GetSettingValue<int>
                (AbpZeroSettingNames.UserManagement.UserLockOut.MaxFailedAccessAttemptsBeforeLockout, tenantId);
        }
        private T GetSettingValue<T>(string settingName, int? tenantId) where T : struct
        {
            return tenantId == null ?
                _settingManager.GetSettingValueForApplication<T>(settingName) :
                _settingManager.GetSettingValueForTenant<T>(settingName, tenantId.Value);
        }
        private bool IsTrue(string settingName, int? tenantId)
        {
            return GetSettingValue<bool>(settingName, tenantId);
        }
        public override async Task<IdentityResult> CreateAsync(TUser user)
        {
            return await base.CreateAsync(user);
        }
        /// <summary>
        /// 检验用户是否有该权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="permissionName"></param>
        /// <returns></returns>
        public virtual async Task<bool> IsGrantedAsync(long userId, string permissionName)
        {
            return await IsGrantedAsync(userId,
                _permissionManager.GetPermission(permissionName));
        }
        /// <summary>
        /// 检验用户是否有该权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="permission"></param>
        /// <returns></returns>
        public virtual async Task<bool> IsGrantedAsync(long userId, Permission permission)
        {
            if (!permission.MultiTenancySides.HasFlag(AbpSession.MultiTenancySide))
            {
                return false;
            }
            var cacheItem = await GetUserPermissionCacheItemAsync(userId);
            if (cacheItem == null)
                return false;
            if(cacheItem.GrantedPermissions.Contains(permission.Name))
            {
                return true;
            }
            if(cacheItem.ProhibitedPermissions.Contains(permission.Name))
            {
                return false;
            }
            foreach(var roleId in cacheItem.RoleIds)
            {
                if(await RoleManager.IsGrantedAsync(roleId,permission))
                {
                    return true;
                }
            }
            return false;
        }
        private async Task<UserPermissionCacheItem> GetUserPermissionCacheItemAsync(long userId)
        {
            var cacheKye = userId + "@" + (GetCurrentTenantId() ?? 0);
            return await _cacheManager.GetUserPermissionCache().GetAsync(cacheKye, async () =>
             {
                 var user = await FindByIdAsync(userId);
                 if (user == null)
                 {
                     return null;
                 }
                 var newCacheItem = new UserPermissionCacheItem(userId);
                 foreach (var roleName in await GetRolesAsync(userId))
                 {
                     newCacheItem.RoleIds.Add((await RoleManager.GetRoleByNameAsync(roleName)).Id);
                 }
                 foreach(var permissionInfo in await UserPermissionStore.GetPermissionsAsync(userId))
                 {
                     if(permissionInfo.IsGranted)
                     {
                         newCacheItem.GrantedPermissions.Add(permissionInfo.Name);
                     }
                     else
                     {
                         newCacheItem.ProhibitedPermissions.Add(permissionInfo.Name);
                     }
                 }
                 return newCacheItem;
             });
        }
        private int? GetCurrentTenantId()
        {
            if (_unitOfWorkManager.Current != null)
            {
                return _unitOfWorkManager.Current.GetTenantId();
            }

            return AbpSession.TenantId;
        }
        /// <summary>
        /// 根据ID获取用户信息.
        /// 如果没有，抛出异常
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>User</returns>
        /// <exception cref="AbpException">如果没有，抛出异常</exception>
        public virtual async Task<TUser> GetUserByIdAsync(long userId)
        {
            var user = await FindByIdAsync(userId);
            if (user == null)
            {
                throw new AbpException("There is no user with id: " + userId);
            }

            return user;
        }
        public virtual async Task<IdentityResult> SetRoles(TUser user,string[] roleNames)
        {
            ///移除角色
            foreach(var userRole in user.Roles.ToList())
            {
                var role = await RoleManager.FindByIdAsync(userRole.RoleId);
                if(roleNames.All(roleName=>role.Name!=roleName))
                {
                    var result = await RemoveFromRolesAsync(user.Id, role.Name);
                    if (!result.Succeeded)
                    {
                        return result;
                    }
                }
            }
            ///添加角色
            //foreach(var userRole in user.Roles.ToList())
            //{
            //    var role = await RoleManager.FindByIdAsync(userRole.RoleId);
            //    if(roleNames.All(roleName => role.Name != roleName))
            //    {
            //        var result=await AddToRoleAsync(user.Id,)
            //    }
            //}
            foreach(var roleName in roleNames)
            {
                var role = await RoleManager.GetRoleByNameAsync(roleName);
                if(user.Roles.All(ur=>ur.RoleId!=role.Id))
                {
                    var result = await AddToRoleAsync(user.Id, roleName);
                    if (!result.Succeeded)
                    {
                        return result;
                    }
                }
            }
            return IdentityResult.Success;
        }
        #endregion
    }
}
