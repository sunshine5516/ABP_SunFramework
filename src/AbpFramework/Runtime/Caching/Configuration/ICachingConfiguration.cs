using AbpFramework.Configuration.Startup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Runtime.Caching.Configuration
{
    /// <summary>
    /// 缓存配置接口，提供完成cache的配置的方法
    /// </summary>
    public interface ICachingConfiguration
    {
        /// <summary>
        /// 获取ABP配置对象
        /// </summary>
        IAbpStartupConfiguration AbpConfiguration { get; }
        /// <summary>
        /// 所有注册的配置名单
        /// </summary>
        IReadOnlyList<ICacheConfigurator> Configurators { get; }
        /// <summary>
        /// 配置所有缓存
        /// </summary>
        /// <param name="initAction">
        /// 配置缓存的action
        /// 缓存被创建后调用
        /// </param>
        void ConfigureAll(Action<ICache> initAction);
        /// <summary>
        /// 用于配置特定的缓存。 
        /// </summary>
        /// <param name="cacheName">Cache name</param>
        /// <param name="initAction">
        /// 配置缓存的action
        /// 缓存被创建后调用
        /// </param>
        void Configure(string cacheName, Action<ICache> initAction);
    }
}
