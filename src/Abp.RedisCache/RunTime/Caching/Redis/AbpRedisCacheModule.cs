using AbpFramework;
using AbpFramework.Modules;
using AbpFramework.Reflection;
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
