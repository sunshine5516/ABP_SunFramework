using AbpFramework.Authorization;
using AbpFramework.Collections;
namespace AbpFramework.Configuration.Startup
{
    /// <summary>
    /// 用于配置授权系统
    /// </summary>
    public interface IAuthorizationConfiguration
    {
        /// <summary>
        /// 授权提供列表
        /// </summary>
        ITypeList<AuthorizationProvider> Providers { get; }
        bool IsEnabled { get; set; }
    }
}
