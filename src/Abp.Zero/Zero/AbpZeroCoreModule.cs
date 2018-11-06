using Abp.Zero.Common.Zero;
using AbpFramework.Modules;
namespace Abp.Zero.Zero
{
    [DependsOn(typeof(AbpZeroCommonModule))]
    public class AbpZeroCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            base.PreInitialize();
        }
        public override void Initialize()
        {
            base.Initialize();
        }
    }
}
