using AbpFramework.Configuration.Startup;
using AbpFramework.Dependency;
using System.Collections.Generic;
namespace AbpFramework.Application.Navigation
{
    /// <summary>
    /// 实现了INavigationManager，运行时是一个单例对象。完成菜单集的初始化。
    /// navigationManager在Initialize方法中先从configuration中获取NavigationProvider派生类的type,
    /// 然后通过容器生成该类型的实例，并调用NavigationProvider实例的SetNavigation完成菜单项的初始化。
    /// </summary>
    internal class NavigationManager : INavigationManager, ISingletonDependency
    {
        #region 声明实例
        public IDictionary<string, MenuDefinition> Menus { get; private set; }
        public MenuDefinition MainMenu
        {
            get { return Menus["MainMenu"]; }
        }
        private readonly IIocResolver _iocResolver;
        private readonly INavigationConfiguration _configuration;
        #endregion
        #region 构造函数
        public NavigationManager(IIocResolver iocResolver
            , INavigationConfiguration configuration)
        {
            this._iocResolver = iocResolver;
            this._configuration = configuration;
            Menus = new Dictionary<string, MenuDefinition>
            {
                {"MainMenu",new MenuDefinition("MainMenu","MainMenu") }
            };
        }
        #endregion
        #region 方法
        public void Initialize()
        {
            var context = new NavigationProviderContext(this);
            foreach(var providerType in _configuration.Providers)
            {
                using (var provider = _iocResolver.ResolveAsDisposable<NavigationProvider>(providerType))
                {
                    provider.Object.SetNavigation(context);
                }
            }
        }
        #endregion

    }
}
