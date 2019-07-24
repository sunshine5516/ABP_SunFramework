using Abp.AutoMapper.AutoMapper;
using AbpFramework.Modules;
using BackgroundJobAndNotificationsDemo.Core;
using System.Reflection;
namespace BackgroundJobAndNotificationsDemo.Application
{
    [DependsOn(typeof(BackgroundJobAndNotificationsDemoCoreModule), typeof(AbpAutoMapperModule))]
    public class BackgroundJobAndNotificationsDemoApplicationModule: AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
