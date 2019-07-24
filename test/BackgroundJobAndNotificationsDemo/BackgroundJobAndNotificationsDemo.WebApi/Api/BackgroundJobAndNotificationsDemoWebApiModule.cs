using Abp.Configuration.Startup;
using Abp.Web.Api;
using AbpFramework.Application.Services;
using AbpFramework.Modules;
using BackgroundJobAndNotificationsDemo.Application;
using System.Reflection;
using System.Web.Http;

namespace BackgroundJobAndNotificationsDemo.WebApi.Api
{
    [DependsOn(typeof(AbpWebApiModule), typeof(BackgroundJobAndNotificationsDemoApplicationModule))]
    public class BackgroundJobAndNotificationsDemoWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
             .ForAll<IApplicationService>(typeof(BackgroundJobAndNotificationsDemoApplicationModule).Assembly, "app")
             .Build();
            Configuration.Modules.AbpWebApi().HttpConfiguration.Filters.Add(new HostAuthenticationFilter("Bearer"));
        }
    }
}
