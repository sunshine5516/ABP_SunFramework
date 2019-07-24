using Abp.Hangfire.Hangfire;
using Abp.Web.Mvc.Web.Mvc;
using AbpFramework.Modules;
using Abp.Hangfire.Hangfire.Configuration;
using Hangfire;
using System.Reflection;
using System.Web.Optimization;
using System.Web.Mvc;
using System.Web.Routing;
using Abp.Web.SignalR.Web.SignalR;
using AbpDemo.EntityFramework;
using AbpDemo.Application;
using AbpDemo.WebApi;
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
       typeof(AbpHangfireModule))]
    public class BackgroundJobAndNotificationsDemoWebModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();
            Configuration.Navigation.Providers.Add<BackgroundJobAndNotificationsDemoNavigationProvider>();
            Configuration.BackgroundJobs.UseHangfire(configuration => //Configure to use hangfire for background jobs.
            {
                configuration.GlobalConfiguration.UseSqlServerStorage("Default"); //Set database connection
            });
            IocManager.AddConventionalRegistrar(new ControllerConventionalRegistrar());
        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            //ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(IocManager));
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