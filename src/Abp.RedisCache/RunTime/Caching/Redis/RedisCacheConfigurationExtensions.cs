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

   public static  class RedisCacheConfigurationExtensions
    {
        /// <summary>
        /// 设置使用redis
        /// </summary>
        /// <param name="cachingConfiguration"></param>
        public static void UseRedis(this ICachingConfiguration cachingConfiguration)
        {
            cachingConfiguration.UseRedis(optionsAction => { });
        }
        public static void UseRedis(this ICachingConfiguration cachingConfiguration, Action<AbpRedisCacheOptions> optionsAction)
        {
            var iocManager = cachingConfiguration.AbpConfiguration.IocManager;
            iocManager.RegisterIfNot<ICacheManager, AbpRedisCacheManager>();
            optionsAction(iocManager.Resolve<AbpRedisCacheOptions>());
        }
    }
}
