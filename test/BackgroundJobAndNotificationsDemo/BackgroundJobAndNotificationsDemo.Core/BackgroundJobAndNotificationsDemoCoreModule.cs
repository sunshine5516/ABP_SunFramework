using Abp.Zero.Zero;
using System.Reflection;
using AbpFramework.Modules;
using AbpFramework.Threading.BackgroundWorkers;
using BackgroundJobAndNotificationsDemo.Core.Users;
using BackgroundJobAndNotificationsDemo.Core.Authorization;
namespace BackgroundJobAndNotificationsDemo.Core
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class BackgroundJobAndNotificationsDemoCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.MultiTenancy.IsEnabled = true;
            //AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);
            Configuration.Authorization.Providers.Add<BackgroundJobAndNotificationsDemoAuthorizationProvider>();
        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
        public override void PostInitialize()
        {
            var workManager=IocManager.Resolve<IBackgroundWorkerManager>();
            workManager.Add(IocManager.Resolve<MakeInactiveUsersPassiveWorker>());
        }
    }
}
