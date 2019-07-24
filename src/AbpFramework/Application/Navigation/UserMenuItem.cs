using System.Collections.Generic;
namespace AbpFramework.Application.Navigation
{
    /// <summary>
    /// 封装了用于显示给用户的菜单/以及子菜单集合
    /// </summary>
    public class UserMenuItem
    {
        /// <summary>
        /// 唯一名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 显示顺序，可选
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// 选择此菜单项时要导航的URL。
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// A custom object related to this menu item.
        /// </summary>
        public object CustomData { get; set; }
        /// <summary>
        /// Target of the menu item. Can be "_blank", "_self", "_parent", "_top" or a frame name.
        /// </summary>
        public string Target { get; set; }
        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsEnabled{ get; set; }
        /// <summary>
        /// 是否可见
        /// </summary>
        public bool IsVisible { get; set; }
        /// <summary>
        /// 子菜单
        /// </summary>
        public IList<UserMenuItem> Items { get; private set; }
        #region 构造函数
        public UserMenuItem()
        {

        }
        internal UserMenuItem(MenuItemDefinition menuItemDefinition)
        {
            Name = menuItemDefinition.Name;
            Icon = menuItemDefinition.Icon;
            DisplayName = menuItemDefinition.DisplayName;
            Order = menuItemDefinition.Order;
            Url = menuItemDefinition.Url;
            CustomData = menuItemDefinition.CustomData;
            Target = menuItemDefinition.Target;
            IsEnabled = menuItemDefinition.IsEnabled;
            IsVisible = menuItemDefinition.IsVisible;
            Items = new List<UserMenuItem>();
        }
        #endregion
    }
}
