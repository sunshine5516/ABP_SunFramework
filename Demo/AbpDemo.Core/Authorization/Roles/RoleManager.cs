using Abp.Zero.Authorization.Roles;
using AbpDemo.Core.Authorization.Users;
using AbpFramework.Authorization;
using AbpFramework.Runtime.Caching;

namespace AbpDemo.Core.Authorization.Roles
{
    public class RoleManager:AbpRoleManager<Role,User>
    {
        public RoleManager(
            RoleStore store,
            IPermissionManager permissionManager,
            ICacheManager cacheManager)
            : base(
                store,
                permissionManager,
                cacheManager)
        {
        }
    }
}
