using AbpFramework.Application.Navigation;
namespace AbpDemo.Web.Models.Layout
{
    public class SideBarNavViewModel
    {
        public UserMenu MainMenu { get; set; }
        public string ActiveMenuItemName { get; set; }
    }
}