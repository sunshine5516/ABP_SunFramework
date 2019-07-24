using Abp.Zero.Common.Zero;
using AbpFramework.Modules;
using System.Reflection;

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
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
