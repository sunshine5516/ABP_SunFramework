using AbpFramework.Authorization;
using AbpFramework.MultiTenancy;
namespace BackgroundJobAndNotificationsDemo.Core.Authorization
{
    public class BackgroundJobAndNotificationsDemoAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            var pages = context.GetPermissionOrNull(PermissionNames.Pages);
            if (pages == null)
            {
                pages = context.CreatePermission(PermissionNames.Pages, ("Pages"));
            }

            //Host permissions
            var tenants = pages.CreateChildPermission(PermissionNames.Pages_Tenants, ("Tenants"), multiTenancySides: MultiTenancySides.Host);
        }
    }
}
