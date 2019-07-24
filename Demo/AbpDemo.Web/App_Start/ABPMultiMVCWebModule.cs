using Abp.RedisCache.RunTime.Caching.Redis;
using Abp.Web.Mvc.Web.Mvc;
using Abp.Web.Mvc.Web.Mvc.Controllers;
using Abp.Web.Web;
using AbpDemo.Application;
using AbpDemo.EntityFramework;
using AbpDemo.WebApi;
using AbpFramework.Modules;
using Castle.MicroKernel.Registration;
using Microsoft.Owin.Security;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Abp.Hangfire.Hangfire.Configuration;
using Hangfire;
using Abp.Hangfire.Hangfire;
using Abp.Web.SignalR.Web.SignalR;

namespace AbpDemo.Web.App_Start
{

    [DependsOn(
        typeof(AbpDemoDataModule),
        typeof(AbpDemoApplicationModule),
        typeof(AbpDemoWebApiModule),
        typeof(AbpRedisCacheModule),

        typeof(AbpWebMvcModule),
        typeof(AbpWebModule),
        typeof(AbpWebSignalRModule),
        typeof(AbpHangfireModule))]
    public class ABPMultiMVCWebModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Navigation.Providers.Add<ABPMultiMVCNavigationProvider>();
            Configuration.BackgroundJobs.UseHangfire(configuration => //启用hangfire，替代默认后台作业管理器
            {
                configuration.GlobalConfiguration.UseSqlServerStorage("Default"); //设置数据库
            });
            //Configuration.Auditing.IsEnabledForAnonymousUsers = true;
            //配置使用Redis缓存
            //IocManager.Register<ICacheManager, AbpRedisCacheManager>();
            Configuration.Caching.UseRedis();
            //完成IWindsorInstaller接口中的注册
            //ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(IocManager));
            //ControllerBuilder.Current.SetControllerFactory(IocManager);
            IocManager.AddConventionalRegistrar(new ControllerConventionalRegistrar());
            //base.PreInitialize();
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
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        public override void PostInitialize()
        {
            base.PostInitialize();
        }
    }
}