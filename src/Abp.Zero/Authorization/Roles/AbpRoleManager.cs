using Abp.Zero.Authorization.Users;
using Abp.Zero.Common.Authorization;
using Abp.Zero.Common.Authorization.Roles;
using Abp.Zero.Common.Runtime.Caching;
using Abp.Zero.Common.Zero.Configuration;
using AbpFramework;
using AbpFramework.Authorization;
using AbpFramework.Domain.Services;
using AbpFramework.Domain.Uow;
using AbpFramework.Runtime.Caching;
using AbpFramework.Runtime.Session;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abp.Zero.Authorization.Roles
{
    /// <summary>
    /// 实现ASP.NET Identity Framework <see cref="RoleManager{TRole,TKey}"/>类.
    /// 应该使用适当的泛型参数派生此类
    /// </summary>
    public abstract class AbpRoleManager<TRole, TUser>
        : RoleManager<TRole, int>, IDomainService
        where TRole : AbpRole<TUser>, new()
        where TUser : AbpUser<TUser>
    {
        #region 声明实例
        public IAbpSession AbpSession { get; set; }
        public IRoleManagementConfig RoleManagementConfig { get;private set;}
        private readonly IPermissionManager _permissionManager;
        private readonly ICacheManager _cacheManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private IRolePermissionStore<TRole> RolePermissionStore
        {
            get
            {
                if(!(Store is IRolePermissionStore<TRole>))
                {
                    throw new AbpException("Store is not IRolePermissionStore");
                }
                return Store as IRolePermissionStore<TRole>;
            }
        }
        #endregion
        #region 构造函数
        //protected AbpRoleManager(AbpRoleStore<TRole, TUser> store) : base(store)
        //{

        //}
        protected AbpRoleManager(AbpRoleStore<TRole, TUser> store,
            IPermissionManager permissionManager,
            ICacheManager cacheManager) : base(store)
        {
            this._permissionManager = permissionManager;
            this._cacheManager = cacheManager;
        }
        #endregion
        #region 方法
        public override async Task<IdentityResult> CreateAsync(TRole role)
        {
            //var tenantId = GetCurrentTenantId();
            //if (tenantId.HasValue && !role.TenantId.HasValue)
            //{
            //    role.TenantId = tenantId.Value;
            //}
            //role.CreationTime = DateTime.Now;
            role.TenantId = null;
            return await base.CreateAsync(role);
        }
        public virtual async Task SetGrantedPermissionsAsync
            (TRole role, IEnumerable<Permission> permissions)
        {
            var oldPermissions = await GetGrantedPermissionsAsync(role);
            var newPermissions = permissions.ToArray();
            foreach(var permission in oldPermissions.Where(p=>!newPermissions.
            Contains(p, PermissionEqualityComparer.Instance)))
            {
                await ProhibitPermissionAsync(role, permission);
            }
            foreach(var permission in newPermissions.Where(p=>!oldPermissions.
            Contains(p, PermissionEqualityComparer.Instance)))
            {
                await GrantPermissionAsync(role, permission);
            }
        }
        /// <summary>
        /// 禁止角色的许可
        /// </summary>
        /// <param name="role"></param>
        /// <param name="permission"></param>
        /// <returns></returns>
        public async Task ProhibitPermissionAsync(TRole role, Permission permission)
        {
            if(!await IsGrantedAsync(role.Id,permission))
            {
                return;
            }
            await RolePermissionStore.RemovePermissionAsync(role,
                new PermissionGrantInfo(permission.Name, true));
        }
        /// <summary>
        /// 角色添加权限
        /// </summary>
        /// <param name="role"></param>
        /// <param name="permission"></param>
        /// <returns></returns>
        public async Task GrantPermissionAsync(TRole role, Permission permission)
        {
            if (await IsGrantedAsync(role.Id, permission))
            {
                return;
            }
            await RolePermissionStore.AddPermissionAsync(role, new PermissionGrantInfo(permission.Name, true));
        }
        /// <summary>
        /// 获取角色的授予权限
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public virtual async Task<IReadOnlyList<Permission>>GetGrantedPermissionsAsync(TRole role)
        {
            var permissionList = new List<Permission>();
            foreach(var permission in _permissionManager.GetAllPermissions())
            {
                if(await IsGrantedAsync(role.Id,permission))
                {
                    permissionList.Add(permission);
                }
            }
            return permissionList;
        }
        /// <summary>
        /// 检查某个角色是否有某个权限
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="permission">权限</param>
        /// <returns>如果有，返回true</returns>
        public virtual async Task<bool> IsGrantedAsync(int roleId, Permission permission)
        {
            //获取缓存的权限
            var cacheItem =await GetRolePermissionCacheItemAsync(roleId);
            return cacheItem.GrantedPermissions.Contains(permission.Name);
        }
        /// <summary>
        /// 根据角色名称获取角色.
        /// 如果无则抛出异常.
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <returns>Role</returns>
        /// <exception cref="AbpException">Throws exception if no role with given roleName</exception>
        public virtual async Task<TRole> GetRoleByNameAsync(string roleName)
        {
            var role = await FindByNameAsync(roleName);
            if (role == null)
            {
                throw new AbpException("There is no role with name: " + roleName);
            }
            return role;
        }
        private async Task<RolePermissionCacheItem> GetRolePermissionCacheItemAsync(int roleId)
        {
            //var cacheKey = roleId + "@" + (GetCurrentTenantId() ?? 0);
            var cacheKey = roleId + "@" + ( 0);
            //return await _cacheManager.GetRolePermissionCache().GetAsync(cacheKey, async () =>
            // {
                 var newCacheItem = new RolePermissionCacheItem(roleId);
                 foreach(var permissionInfo in await RolePermissionStore.GetPermissionsAsync(roleId))
                 {
                     if(permissionInfo.IsGranted)
                     {
                         newCacheItem.GrantedPermissions.Add(permissionInfo.Name);
                     }
                 }
                 return newCacheItem;
             //});
        }
        /// <summary>
        /// 根据ID获取角色信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public virtual async Task<TRole> GetRoleByIdAsync(int roleId)
        {
            var role = await FindByIdAsync(roleId);
            if (role == null)
            {
                throw new AbpException("There is no role with id: " + roleId);
            }

            return role;
        }
        private int? GetCurrentTenantId()
        {
            if(_unitOfWorkManager.Current!=null)
            {
                return _unitOfWorkManager.Current.GetTenantId();
            }
            return AbpSession.TenantId;
        }
        #endregion
    }
}
