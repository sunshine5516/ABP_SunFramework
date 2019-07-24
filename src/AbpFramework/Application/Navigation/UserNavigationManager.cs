using AbpFramework.Authorization;
using AbpFramework.Collections.Extensions;
using AbpFramework.Dependency;
using AbpFramework.Runtime.Session;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AbpFramework.Application.Navigation
{
    public class UserNavigationManager : IUserNavigationManager, ITransientDependency
    {
        #region 声明实例
        public IAbpSession AbpSession { get; set; }
        private readonly INavigationManager _navigationManager;
        private readonly IIocResolver _iocResolver;
        #endregion
        #region 构造函数
        public UserNavigationManager(INavigationManager navigationManager,
            IIocResolver iocResolver)
        {
            this._navigationManager = navigationManager;
            this._iocResolver = iocResolver;
        }
        #endregion
        #region 方法
        public async Task<UserMenu> GetMenuAsync(string menuName, UserIdentifier user)
        {
            var menuDefinition=_navigationManager.Menus.GetOrDefault(menuName);
            if(menuDefinition==null)
            {
                throw new AbpException("There is no menu with given name: " + menuName);
            }
            var userMenu = new UserMenu(menuDefinition);
            await FillUserMenuItems(user,menuDefinition.Items, userMenu.Items);
            return userMenu;
        }

        public async Task<IReadOnlyList<UserMenu>> GetMenusAsync(UserIdentifier user)
        {
            var userMenus = new List<UserMenu>();
            foreach(var menu in _navigationManager.Menus.Values)
            {
                userMenus.Add(await GetMenuAsync(menu.Name, user));
            }
            return userMenus;
        }
        private async Task<int> FillUserMenuItems(UserIdentifier user, IList<MenuItemDefinition> menuItemDefinitions, IList<UserMenuItem> userMenuItems)
        {
            //TODO: Can be optimized by re-using FeatureDependencyContext.

            var addedMenuItemCount = 0;

            using (var scope = _iocResolver.CreateScope())
            {
                var permissionDependencyContext = scope.Resolve<PermissionDependencyContext>();
                permissionDependencyContext.User = user;

                //var featureDependencyContext = scope.Resolve<FeatureDependencyContext>();
                //featureDependencyContext.TenantId = user == null ? null : user.TenantId;

                foreach (var menuItemDefinition in menuItemDefinitions)
                {
                    if (menuItemDefinition.RequiresAuthentication && user == null)
                    {
                        continue;
                    }

                    if (!string.IsNullOrEmpty(menuItemDefinition.RequiredPermissionName))
                    {
                        var permissionDependency = new SimplePermissionDependency(menuItemDefinition.RequiredPermissionName);
                        if (user == null || !(await permissionDependency.IsSatisfiedAsync(permissionDependencyContext)))
                        {
                            continue;
                        }
                    }

                    if (menuItemDefinition.PermissionDependency != null &&
                        (user == null || !(await menuItemDefinition.PermissionDependency.IsSatisfiedAsync(permissionDependencyContext))))
                    {
                        continue;
                    }

                    //if (menuItemDefinition.FeatureDependency != null &&
                    //    (AbpSession.MultiTenancySide == MultiTenancySides.Tenant || (user != null && user.TenantId != null)) &&
                    //    !(await menuItemDefinition.FeatureDependency.IsSatisfiedAsync(featureDependencyContext)))
                    //{
                    //    continue;
                    //}

                    var userMenuItem = new UserMenuItem(menuItemDefinition);
                    if (menuItemDefinition.IsLeaf || (await FillUserMenuItems(user, menuItemDefinition.Items, userMenuItem.Items)) > 0)
                    {
                        userMenuItems.Add(userMenuItem);
                        ++addedMenuItemCount;
                    }
                }
            }

            return addedMenuItemCount;
        }
        private async Task<int> FillUserMenuItems(IList<MenuItemDefinition>
            menuItemDefinitions, IList<UserMenuItem> userMenuItems)
        {
            var addedMenuItemCount = 0;
            using (var scope = _iocResolver.CreateScope())
            {
                //var permissionDependencyContext = scope.Resolve<PermissionDependencyContext>();
                //permissionDependencyContext.User = user;
                foreach (var menuItemDefinition in menuItemDefinitions)
                {
                    if (menuItemDefinition.RequiresAuthentication)
                    {
                        continue;
                    }

                    if (!string.IsNullOrEmpty(menuItemDefinition.RequiredPermissionName))
                    {
                        //var permissionDependency = new SimplePermissionDependency(menuItemDefinition.RequiredPermissionName);
                        //var permissionDependency = new SimplePermissionDependency(menuItemDefinition.RequiredPermissionName);
                        //if (user == null || !(await permissionDependency.IsSatisfiedAsync(permissionDependencyContext)))
                        //{
                        //    continue;
                        //}
                    }
                    var userMenuItem = new UserMenuItem(menuItemDefinition);
                    if (menuItemDefinition.IsLeaf || (await FillUserMenuItems(menuItemDefinition.Items, userMenuItem.Items)) > 0)
                    {
                        userMenuItems.Add(userMenuItem);
                        ++addedMenuItemCount;
                    }
                }
            }
            return addedMenuItemCount;
        }
        #endregion

    }
}
