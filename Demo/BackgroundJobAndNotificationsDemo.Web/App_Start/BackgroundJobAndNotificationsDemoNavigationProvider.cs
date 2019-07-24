using AbpDemo.Core.Authorization;
using AbpFramework.Application.Navigation;
namespace BackgroundJobAndNotificationsDemo.Web
{ 
    public class BackgroundJobAndNotificationsDemoNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        "Home",
                        "HomePage",
                        url: "#/",
                        icon: "fa fa-home"
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "Tenants",
                        "Tenants",
                        url: "#tenants",
                        icon: "fa fa-globe",
                        requiredPermissionName: PermissionNames.Pages_Tenants
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "About",
                        "About",
                        url: "#/about",
                        icon: "fa fa-info"
                        )
                );
        }
    }
}