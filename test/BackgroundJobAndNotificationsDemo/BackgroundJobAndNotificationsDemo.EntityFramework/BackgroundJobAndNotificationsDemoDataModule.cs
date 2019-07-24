using Abp.Zero.EntityFramework;
using AbpFramework.Modules;
using BackgroundJobAndNotificationsDemo.Core;
using System.Reflection;
namespace BackgroundJobAndNotificationsDemo.EntityFramework
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(BackgroundJobAndNotificationsDemoCoreModule))]
    public class BackgroundJobAndNotificationsDemoDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
