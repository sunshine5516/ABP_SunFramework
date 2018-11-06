using AbpFramework;
using AbpFramework.Modules;
using AbpFramework.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.RedisCache.RunTime.Caching.Redis
{
    [DependsOn(typeof(AbpKernelModule))]
    public class AbpRedisCacheModule : AbpModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<AbpRedisCacheOptions>();
        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpRedisCacheModule).GetAssembly());
        }
    }
}
