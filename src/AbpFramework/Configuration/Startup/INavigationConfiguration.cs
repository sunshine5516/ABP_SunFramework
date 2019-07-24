using AbpFramework.Application.Navigation;
using AbpFramework.Collections;
namespace AbpFramework.Configuration.Startup
{
    /// <summary>
    /// 导航配置项
    /// </summary>
    public interface INavigationConfiguration
    {
        /// <summary>
        /// List of navigation providers.
        /// </summary>
        ITypeList<NavigationProvider> Providers { get; }
    }
}
