using Abp.Zero.Authorization;
using AbpDemo.Core.Authorization.Roles;
using AbpDemo.Core.Authorization.Users;

namespace AbpDemo.Core.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
