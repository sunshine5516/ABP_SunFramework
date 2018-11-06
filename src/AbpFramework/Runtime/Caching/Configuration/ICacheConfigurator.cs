using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Runtime.Caching.Configuration
{
    /// <summary>
    /// 已注册的缓存配置器。
    /// </summary>
    public interface ICacheConfigurator
    {
        /// <summary>
        /// 缓存名称
        /// 如果此配置程序配置所有缓存，它将为空。
        /// </summary>
        string CacheName { get; }
        Action<ICache> InitAction { get; }
    }
}
