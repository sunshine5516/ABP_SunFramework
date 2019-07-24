using Abp.Zero.Authorization.Roles;
using Abp.Zero.Common.Authorization.Roles;
using Abp.Zero.Common.Authorization.Users;
using AbpDemo.Core.Authorization.Users;
using AbpFramework.Domain.Repositories;

namespace AbpDemo.Core.Authorization.Roles
{
    public class RoleStore : AbpRoleStore<Role, User>
    {
        public RoleStore(
         IRepository<Role> roleRepository,
         IRepository<UserRole, long> userRoleRepository,
         IRepository<RolePermissionSetting, long> rolePermissionSettingRepository)
         : base(
             roleRepository,
             userRoleRepository,
             rolePermissionSettingRepository)
        {
        }
    }
}
