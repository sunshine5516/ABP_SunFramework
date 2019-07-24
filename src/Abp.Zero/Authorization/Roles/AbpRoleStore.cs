using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Zero.Authorization.Users;
using Abp.Zero.Common.Authorization;
using Abp.Zero.Common.Authorization.Roles;
using Abp.Zero.Common.Authorization.Users;
using AbpFramework.Dependency;
using AbpFramework.Domain.Repositories;
using Microsoft.AspNet.Identity;
namespace Abp.Zero.Authorization.Roles
{
    public abstract class AbpRoleStore<TRole, TUser> :
        IQueryableRoleStore<TRole, int>,
        IRolePermissionStore<TRole>,
        ITransientDependency
        where TRole : AbpRole<TUser>
        where TUser : AbpUser<TUser>
    {
        #region 声明实例
        private readonly IRepository<TRole> _roleRepository;
        private readonly IRepository<UserRole, long>_userRoleRepository;
        private readonly IRepository<RolePermissionSetting, long> _rolePermissionSettingRepository;
        public IQueryable<TRole> Roles
        {
            get { return _roleRepository.GetAll(); }
        }

        #endregion
        #region 构造函数
        protected AbpRoleStore(IRepository<TRole> roleRepository,
            IRepository<UserRole, long> userRoleRepository,
            IRepository<RolePermissionSetting, long> rolePermissionSettingRepository)
        {
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _rolePermissionSettingRepository = rolePermissionSettingRepository;
        }
        #endregion
        #region 方法

        #endregion
        
        public virtual async Task AddPermissionAsync(TRole role, PermissionGrantInfo permissionGrantInfo)
        {
            await _rolePermissionSettingRepository.InsertAsync(
                new RolePermissionSetting
                {
                    TenantId = role.TenantId,
                    RoleId = role.Id,
                    Name = permissionGrantInfo.Name,
                    IsGranted = permissionGrantInfo.IsGranted,
                    CreationTime=DateTime.Now
                });
        }

        public virtual async Task CreateAsync(TRole role)
        {
            await _roleRepository.InsertAsync(role);
        }

        public virtual async Task DeleteAsync(TRole role)
        {
            await _userRoleRepository.DeleteAsync(ur => ur.RoleId == role.Id);
            await _roleRepository.DeleteAsync(role);
        }

        public void Dispose()
        {
            //throw new System.NotImplementedException();
        }

        public virtual async Task<TRole> FindByIdAsync(int roleId)
        {
            return await _roleRepository.FirstOrDefaultAsync(roleId);
        }

        public virtual async Task<TRole> FindByNameAsync(string roleName)
        {
            return await _roleRepository.FirstOrDefaultAsync(
                role => role.Name == roleName
                );
        }

        public Task<IList<PermissionGrantInfo>> GetPermissionsAsync(TRole role)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IList<PermissionGrantInfo>> GetPermissionsAsync(int roleId)
        {
            //var model2 = _rolePermissionSettingRepository.GetAllList();
            //var model3 = _rolePermissionSettingRepository.GetAll();
            //var model = (await _rolePermissionSettingRepository.GetAllListAsync())
            //    ;
            return (await _rolePermissionSettingRepository.GetAllListAsync(p=>p.RoleId==roleId))
                .Select(p=>new PermissionGrantInfo(p.Name, p.IsGranted))
                .ToList();
        }

        public Task<bool> HasPermissionAsync(int roleId, PermissionGrantInfo permissionGrant)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveAllPermissionSettingsAsync(TRole role)
        {
            throw new System.NotImplementedException();
        }

        public Task RemovePermissionAsync(TRole role, PermissionGrantInfo permissionGrant)
        {
            throw new System.NotImplementedException();
        }

        public virtual async Task UpdateAsync(TRole role)
        {
            await _roleRepository.UpdateAsync(role);
        }
    }
}
