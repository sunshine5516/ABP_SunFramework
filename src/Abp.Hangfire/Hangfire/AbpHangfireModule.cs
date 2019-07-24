using Abp.Hangfire.Hangfire.Configuration;
using AbpFramework;
using AbpFramework.Modules;
using Hangfire;
using AbpFramework.Reflection;
namespace Abp.Hangfire.Hangfire
{
    [DependsOn(typeof(AbpKernelModule))]
    public class AbpHangfireModule:AbpModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<IAbpHangfireConfiguration, AbpHangfireConfiguration>();
            Configuration.Modules
                .AbpHangfire()
                .GlobalConfiguration
                .UseActivator(new HangfireIocJobActivator(IocManager));
        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpHangfireModule).GetAssembly());
            GlobalJobFilters.Filters.Add(IocManager.Resolve<AbpHangfireJobExceptionFilter>());
        }
    }
}
