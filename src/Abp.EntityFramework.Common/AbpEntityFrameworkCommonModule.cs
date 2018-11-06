using AbpFramework;
using AbpFramework.Modules;
using AbpFramework.Reflection;
namespace Abp.EntityFramework.Common
{
    [DependsOn(typeof(AbpKernelModule))]
    public class AbpEntityFrameworkCommonModule: AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpEntityFrameworkCommonModule).GetAssembly());
        }
    }
}
