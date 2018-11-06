using Abp.Configuration.Startup;
using Abp.Web.Api;
using AbpDemo.Application;
using AbpDemo.Application.Test;
using AbpFramework.Application.Services;
using AbpFramework.Modules;
using System.Reflection;
using System.Web.Http;
namespace AbpDemo.WebApi
{
    [DependsOn(typeof(AbpWebApiModule), typeof(AbpDemoApplicationModule))]
    public class AbpDemoWebApiModule: AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            //base.Initialize();
            //Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
            //   .For<ITestService>("tasksystem/task").ForMethod("testMethod")
            //    .Build();
            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(AbpDemoApplicationModule).Assembly, "app")
                .Build();
            Configuration.Modules.AbpWebApi().HttpConfiguration
                .Filters.Add(new HostAuthenticationFilter("Bearer"));

        }
    }
}
