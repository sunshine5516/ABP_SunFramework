using AbpFramework.Dependency;
namespace AbpFramework.Application.Navigation
{
    /// <summary>
    /// 由改变应用程序导航的类来实现。
    /// 用于设置NavigationManager的Menus和MainMenu（通过 INavigationProviderContext对象访问NavigationManager）。
    /// </summary>
    public abstract class NavigationProvider: ITransientDependency
    {
        /// <summary>
        /// 设置导航
        /// </summary>
        /// <param name="context"></param>
        public abstract void SetNavigation(INavigationProviderContext context);
    }
}
