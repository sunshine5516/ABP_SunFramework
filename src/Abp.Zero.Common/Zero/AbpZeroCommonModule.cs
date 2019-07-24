using Abp.Zero.Common.Zero.Configuration;
using AbpFramework;
using AbpFramework.Modules;
using AbpFramework.Reflection;
namespace Abp.Zero.Common.Zero
{
    [DependsOn(typeof(AbpKernelModule))]
    public class AbpZeroCommonModule : AbpModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<IUserManagementConfig, UserManagementConfig>();
            Configuration.Settings.Providers.Add<AbpZeroSettingProvider>();
            base.PreInitialize();
        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpZeroCommonModule).GetAssembly());
        }
    }
}
