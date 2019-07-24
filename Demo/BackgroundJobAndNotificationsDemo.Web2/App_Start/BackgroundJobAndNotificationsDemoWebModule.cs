using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Abp.Hangfire;
using Abp.Hangfire.Hangfire;
using Abp.Web.Mvc;
using Abp.Web.Mvc.Web.Mvc;
using Abp.Hangfire.Hangfire.Configuration;
using Abp.Web.SignalR.Web.SignalR;
using AbpDemo.Application;
using AbpDemo.EntityFramework;
using AbpDemo.WebApi;
using AbpFramework.Modules;
using Hangfire;
using Abp.Web.Mvc.Web.Mvc.Controllers;
using Castle.MicroKernel.Registration;
using Microsoft.Owin.Security;
using System.Web;

namespace BackgroundJobAndNotificationsDemo.Web
{
    [DependsOn(
        typeof(AbpDemoDataModule),
        typeof(AbpDemoApplicationModule),
        typeof(AbpDemoWebApiModule),
        typeof(AbpWebMvcModule),
        typeof(AbpWebSignalRModule), //Add AbpWebSignalRModule dependency
        typeof(AbpHangfireModule))] //Add AbpHangfireModule dependency
    public class BackgroundJobAndNotificationsDemoWebModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Enable database based localization
            
            //Configure navigation/menu
            Configuration.Navigation.Providers.Add<BackgroundJobAndNotificationsDemoNavigationProvider>();

            //Configuration.BackgroundJobs.IsJobExecutionEnabled = false; //Can disable job manager to not process jobs.

            Configuration.BackgroundJobs.UseHangfire(configuration => //Configure to use hangfire for background jobs.
            {
                configuration.GlobalConfiguration.UseSqlServerStorage("Default"); //Set database connection
            });
            IocManager.AddConventionalRegistrar(new ControllerConventionalRegistrar());
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            IocManager.IocContainer.Register(
               Component.For<IAuthenticationManager>()
               .UsingFactoryMethod(() => HttpContext.Current.GetOwinContext().Authentication)
               .LifestyleTransient()
               );
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
