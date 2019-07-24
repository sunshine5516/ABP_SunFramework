using AbpFramework.Authorization;
using AbpFramework.MultiTenancy;
namespace AbpDemo.Core.Authorization
{
    public class ABPMultiMVCAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, "Userssss");
            context.CreatePermission(PermissionNames.Pages_Roles, "Roles");
            context.CreatePermission(PermissionNames.Pages_Tenants, "Tenants", multiTenancySides: MultiTenancySides.Host);
        }
    }
}
