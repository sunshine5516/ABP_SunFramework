using AbpFramework;
using AbpFramework.Modules;
namespace Abp.Zero.Common.Zero
{
    [DependsOn(typeof(AbpKernelModule))]
    public class AbpZeroCommonModule : AbpModule
    {
        public override void PreInitialize()
        {
            base.PreInitialize();
        }
        public override void PostInitialize()
        {
            base.PostInitialize();
        }
    }
}
