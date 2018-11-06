using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Runtime.Caching
{
    /// <summary>
    /// 缓存管理接口，该接口和实现用于生成，配置以及销毁ICache实例
    /// 具体是通过ICachingConfiguration对象来初始化cache,并通过ConcurrentDictionary<string, ICache>来存放和管理cache.
    /// </summary>
    public interface ICacheManager : IDisposable
    {
        /// <summary>
        /// 获取所有缓存
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<ICache> GetAllCaches();
        /// <summary>
        /// 返回一个ICache实例
        /// 可能会创建缓存，如果它不存在。
        /// </summary>
        /// <param name="name">
        /// 唯一名称
        /// </param>
        /// <returns>The cache reference</returns>
        [NotNull] ICache GetCache([NotNull] string name);
    }
}
