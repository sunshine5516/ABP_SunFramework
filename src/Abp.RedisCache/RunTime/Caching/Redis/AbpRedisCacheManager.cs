using AbpFramework.Dependency;
using AbpFramework.Runtime.Caching;
using AbpFramework.Runtime.Caching.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.RedisCache.RunTime.Caching.Redis
{
    public class AbpRedisCacheManager : CacheManagerBase
    {
        #region 构造函数
        public AbpRedisCacheManager(IIocManager iocManager, ICachingConfiguration configuration)
           : base(iocManager, configuration)
        {
            _IocManager.RegisterIfNot<AbpRedisCache>(DependencyLifeStyle.Transient);
        }
        #endregion
        protected override ICache CreateCacheImplementation(string name)
        {
            return _IocManager.Resolve<AbpRedisCache>(new { name });
        }
    }
}
