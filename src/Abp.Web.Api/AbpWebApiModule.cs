using Abp.Configuration.Startup;
using Abp.Web.Web;
using Abp.WebApi.Auditing;
using Abp.WebApi.Configuration;
using Abp.WebApi.Controllers;
using Abp.WebApi.Controllers.Dynamic;
using Abp.WebApi.Controllers.Dynamic.Builders;
using Abp.WebApi.Controllers.Dynamic.Selectors;
using AbpFramework.Modules;
using Castle.MicroKernel.Registration;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Abp.Web.Api
{
    [DependsOn(typeof(AbpWebModule))]
    public class AbpWebApiModule: AbpModule
    {
        public override void PreInitialize()
        {
            IocManager.AddConventionalRegistrar(new ApiControllerConventionalRegistrar());
            IocManager.Register<IDynamicApiControllerBuilder, DynamicApiControllerBuilder>();
            IocManager.Register<IAbpWebApiConfiguration, AbpWebApiConfiguration>();
            //Configuration.Modules

            //base.PreInitialize();
        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
        public override void PostInitialize()
        {
            var httpConfiguration= IocManager.Resolve<IAbpWebApiConfiguration>().HttpConfiguration;
            InitializeRoutes(httpConfiguration);
            InitializeAspNetServices(httpConfiguration);
            InitializeFilters(httpConfiguration);

            foreach (var controllerInfo in IocManager.Resolve<DynamicApiControllerManager>().GetAll())
            {
                IocManager.IocContainer.Register(
                    Component.For(controllerInfo.InterceptorType).LifestyleTransient(),
                    Component.For(controllerInfo.ApiControllerType)
                        .Proxy.AdditionalInterfaces(controllerInfo.ServiceInterfaceType)
                        .Interceptors(controllerInfo.InterceptorType)
                        .LifestyleTransient()
                    );

                //LogHelper.Logger.DebugFormat("Dynamic web api controller is created for type '{0}' with service name '{1}'.", controllerInfo.ServiceInterfaceType.FullName, controllerInfo.ServiceName);
            }

            Configuration.Modules.AbpWebApi().HttpConfiguration.EnsureInitialized();
            //base.PostInitialize();
        }


        #region 辅助方法
        private static void InitializeRoutes(HttpConfiguration httpConfiguration)
        {
            ///动态webApi
            httpConfiguration.Routes.MapHttpRoute(
                name: "AbpDynamicWebApi",
                 routeTemplate: "api/services/{*serviceNameWithAction}"
                 //routeTemplate: "api/services/{serviceName}/{Action}"
                );
            ///其他路由
            httpConfiguration.Routes.MapHttpRoute(
                name: "AbpCacheController_Clear",
                routeTemplate: "api/AbpCache/Clear",
                defaults: new { controller = "AbpCache", action = "Clear" }
                );
            httpConfiguration.Routes.MapHttpRoute(
                name: "AbpCacheController_ClearAll",
                routeTemplate: "api/AbpCache/ClearAll",
                defaults: new { controller = "AbpCache", action = "ClearAll" }
                );
        }
        /// <summary>
        /// 注册webApi服务，用<see cref="IHttpControllerSelector"/>替代默认的Selector
        /// </summary>
        /// <param name="httpConfiguration"></param>
        private void InitializeAspNetServices(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.Services.Replace(typeof(IHttpControllerSelector), new AbpHttpControllerSelector(httpConfiguration, IocManager.Resolve<DynamicApiControllerManager>()));
            httpConfiguration.Services.Replace(typeof(IHttpActionSelector), new AbpApiControllerActionSelector(IocManager.Resolve<IAbpWebApiConfiguration>()));
            httpConfiguration.Services.Replace(typeof(IHttpControllerActivator), new AbpApiControllerActivator(IocManager));
            //httpConfiguration.Services.Replace(typeof(IApiExplorer), IocManager.Resolve<AbpApiExplorer>());
        }
        private void InitializeFilters(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.Filters.Add(IocManager.Resolve<AbpApiAuditFilter>());

            httpConfiguration.MessageHandlers.Add(IocManager.Resolve<ResultWrapperHandler>());
        }
        #endregion
    }
}
