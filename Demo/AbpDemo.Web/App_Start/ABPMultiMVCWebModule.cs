using Abp.RedisCache.RunTime.Caching.Redis;
using Abp.Web.Mvc.Web.Mvc.Controllers;
using AbpDemo.Application;
using AbpDemo.EntityFramework;
using AbpDemo.WebApi;
using AbpFramework.Modules;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AbpDemo.Web.App_Start
{

    [DependsOn(
        typeof(AbpDemoApplicationModule),
        typeof(AbpDemoWebApiModule),
       typeof(AbpRedisCacheModule),
        typeof(AbpDemoDataModule))]
    public class ABPMultiMVCWebModule : AbpModule
    {
        public override void PreInitialize()
        {
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
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(IocManager));
            IocManager.IocContainer.Register(
                
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