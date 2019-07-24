using AbpFramework.Authorization;
using AbpFramework.Collections.Extensions;
using System;
using System.Collections.Generic;
namespace AbpFramework.Application.Navigation
{
    /// <summary>
    /// 封装了主菜单的子菜单的属性。子菜单可以引用其他子菜单构成一个菜单树
    /// </summary>
    public class MenuItemDefinition: IHasMenuItemDefinitions
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
        /// 顺序
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 导航地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 权限名称，只有拥有该权限的用户才能访问
        /// Optional.
        /// </summary>
        [Obsolete("Use PermissionDependency instead.")]
        public string RequiredPermissionName { get; set; }

        /// <summary>
        /// 权限依赖。 只有能够满足此权限依赖性的用户才能看到此菜单项。
        /// Optional.
        /// </summary>
        public IPermissionDependency PermissionDependency { get; set; }

        /// <summary>
        /// 功能依赖
        /// Optional.
        /// </summary>
        //public IFeatureDependency FeatureDependency { get; set; }

        /// <summary>
        /// 如果只有经过身份验证的用户才能看到此菜单项，则可以将其设置为true。
        /// </summary>
        public bool RequiresAuthentication { get; set; }

        /// <summary>
        /// 如果无子项，返回ture
        /// </summary>
        public bool IsLeaf => Items.IsNullOrEmpty();

        /// <summary>
        /// Target of the menu item. Can be "_blank", "_self", "_parent", "_top" or a frame name.
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// Can be used to store a custom object related to this menu item. Optional.
        /// </summary>
        public object CustomData { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 是否可见
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// 此菜单项的子项，可选的。
        /// </summary>
        public virtual IList<MenuItemDefinition> Items { get; }

        /// <param name="name"></param>
        /// <param name="displayName"></param>
        /// <param name="icon"></param>
        /// <param name="url"></param>
        /// <param name="requiresAuthentication"></param>
        /// <param name="requiredPermissionName">This parameter is obsolete. Use <paramref name="permissionDependency"/> instead!</param>
        /// <param name="order"></param>
        /// <param name="customData"></param>
        /// <param name="featureDependency"></param>
        /// <param name="target"></param>
        /// <param name="isEnabled"></param>
        /// <param name="isVisible"></param>
        /// <param name="permissionDependency"></param>
        public MenuItemDefinition(
            string name,
            string displayName,
            string icon = null,
            string url = null,
            bool requiresAuthentication = false,
            string requiredPermissionName = null,
            int order = 0,
            object customData = null,
            string target = null,
            bool isEnabled = true,
            bool isVisible = true)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(displayName, nameof(displayName));

            Name = name;
            DisplayName = displayName;
            Icon = icon;
            Url = url;
            RequiresAuthentication = requiresAuthentication;
            RequiredPermissionName = requiredPermissionName;
            Order = order;
            CustomData = customData;
            Target = target;
            IsEnabled = isEnabled;
            IsVisible = isVisible;
            Items = new List<MenuItemDefinition>();
        }

        /// <summary>
        /// 添加 <see cref="MenuItemDefinition"/> to <see cref="Items"/>.
        /// </summary>
        /// <param name="menuItem"><see cref="MenuItemDefinition"/> to be added</param>
        /// <returns>This <see cref="MenuItemDefinition"/> object</returns>
        public MenuItemDefinition AddItem(MenuItemDefinition menuItem)
        {
            Items.Add(menuItem);
            return this;
        }
    }
}
