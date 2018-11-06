using AbpFramework.Dependency;
using AbpFramework.Runtime.Caching.Configuration;
using JetBrains.Annotations;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Runtime.Caching
{
    public abstract class CacheManagerBase : ICacheManager, ISingletonDependency
    {
        #region 声明实例
        protected readonly IIocManager _IocManager;
        protected readonly ConcurrentDictionary<string, ICache> Caches;
        protected readonly ICachingConfiguration Configuration;
        #endregion
        #region 构造函数
        protected CacheManagerBase(IIocManager iocManager, ICachingConfiguration configuration)
        {
            this._IocManager = iocManager;
            Configuration = configuration;
            Caches = new ConcurrentDictionary<string, ICache>();
        }
        #endregion
        #region 方法       
        public IReadOnlyList<ICache> GetAllCaches()
        {
            return Caches.Values.ToImmutableList();
        }

        public ICache GetCache([NotNull] string name)
        {
            return Caches.GetOrAdd(name, (cacheName) =>
             {
                 var cache = CreateCacheImplementation(cacheName);
                 var configurators = Configuration.Configurators.Where(c => c.CacheName == null || c.CacheName == cacheName);
                 foreach (var configurator in configurators)
                 {
                     configurator.InitAction?.Invoke(cache);
                 }

                 return cache;
             });
        }
        /// <summary>
        /// 用于创建实际的缓存实现。
        /// </summary>
        /// <param name="name">缓存名称</param>
        /// <returns>Cache object</returns>
        protected abstract ICache CreateCacheImplementation(string name);
        public virtual void Dispose()
        {
            foreach (var cache in Caches)
            {
                _IocManager.Release(cache);
            }
            Caches.Clear();
        }
        #endregion

    }
}
