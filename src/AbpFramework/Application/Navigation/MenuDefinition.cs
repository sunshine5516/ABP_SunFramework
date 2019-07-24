using System;
using System.Collections.Generic;
namespace AbpFramework.Application.Navigation
{
    /// <summary>
    /// 封装了导航栏上的主菜单的属性。
    /// ABP通过MenuDefinition/MenuItemDefinition构成了完整的系统菜单集合（超集）。
    /// 而UserMenu/UserMenuItem只构成用户所能访问的菜单集合，并且其DisplayName是本地化以后的DisplayName
    /// </summary>
    public class MenuDefinition: IHasMenuItemDefinitions
    {
        /// <summary>
        /// 唯一名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 可用于存储与此菜单相关的自定义对象。 可选的。
        /// </summary>
        public object CustomData { get; set; }
        /// <summary>
        /// 菜单项目（第一级）。
        /// </summary>
        public IList<MenuItemDefinition> Items { get; set; }
        public MenuDefinition AddItem(MenuItemDefinition menuItem)
        {
            Items.Add(menuItem);
            return this;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">唯一名称</param>
        /// <param name="displayName">显示名称</param>
        /// <param name="customData">Can be used to store a custom object related to this menu.</param>
        public MenuDefinition(string name, string displayName, object customData = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name", "Menu name can not be empty or null.");
            }

            if (displayName == null)
            {
                throw new ArgumentNullException("displayName", "Display name of the menu can not be null.");
            }

            Name = name;
            DisplayName = displayName;
            CustomData = customData;

            Items = new List<MenuItemDefinition>();
        }
    }
}
