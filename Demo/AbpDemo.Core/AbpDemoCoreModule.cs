using Abp.Zero.Zero;
using AbpDemo.Core.Authorization;
using AbpDemo.Core.Authorization.Users;
using AbpDemo.Core.Configuration;
using AbpFramework.Modules;
using AbpFramework.Threading.BackgroundWorkers;
using System.Reflection;
namespace AbpDemo.Core
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class AbpDemoCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;
            //Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant); 
            Configuration.Authorization.Providers.Add<ABPMultiMVCAuthorizationProvider>();
            Configuration.MultiTenancy.IsEnabled = AbpDemoConsts.MultiTenancyEnabled;
            Configuration.Settings.Providers.Add<AppSettingProvider>();
        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
        public override void PostInitialize()
        {
            var workManager = IocManager.Resolve<IBackgroundWorkerManager>();
            workManager.Add(IocManager.Resolve<MakeInactiveUsersPassiveWorker>());
        }
    }
}
