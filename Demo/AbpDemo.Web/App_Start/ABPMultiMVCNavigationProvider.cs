using AbpDemo.Core.Authorization;
using AbpFramework.Application.Navigation;
namespace AbpDemo.Web.App_Start
{
    public class ABPMultiMVCNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                new MenuItemDefinition(
                    PageNames.Home,
                    "主页",
                    url: "",
                    icon: "home"
                    ).AddItem(
                    new MenuItemDefinition(
                        PageNames.About,
                        "About",
                        url: "About",
                        icon: "info"
                    )
                )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Users,
                        "Users",
                        url: "Users",
                        icon: "people",
                        requiredPermissionName: PermissionNames.Pages_Users
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Roles,
                        "Roles",
                        url: "Roles",
                        icon: "local_offer",
                        requiredPermissionName: PermissionNames.Pages_Roles
                    )
                );
        }
    }
}