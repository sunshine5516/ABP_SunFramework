using AbpFramework;
using AbpFramework.Modules;
using AbpFramework.Reflection.Extensions;
namespace Abp.TestBase.TestBase
{
    [DependsOn(typeof(AbpKernelModule))]
    public class AbpTestBaseModule:AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.EventBus.UseDefaultEventBus = false;
            Configuration.DefaultNameOrConnectionString = "Default";
        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpTestBaseModule).GetAssembly());
        }
    }
}
