using System.Collections.Generic;
namespace AbpFramework.Application.Navigation
{
    /// <summary>
    /// 为具有菜单项的类声明通用接口
    /// </summary>
    public interface IHasMenuItemDefinitions
    {
        IList<MenuItemDefinition> Items { get; }
    }
}
