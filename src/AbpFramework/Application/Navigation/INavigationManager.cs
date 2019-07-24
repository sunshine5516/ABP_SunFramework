using System.Collections.Generic;
namespace AbpFramework.Application.Navigation
{
    /// <summary>
    /// 在应用程序中管理导航接口。
    /// 这个接口封装了一个字典类型的Menus和MenuDefinition类型的MainMenu
    /// </summary>
    public interface INavigationManager
    {
        /// <summary>
        /// 应用程序中定义的所有菜单。
        /// </summary>
        IDictionary<string,MenuDefinition> Menus { get; }
        /// <summary>
        /// 获取应用程序的主菜单。
        /// </summary>
        MenuDefinition MainMenu { get; }
    }
}
