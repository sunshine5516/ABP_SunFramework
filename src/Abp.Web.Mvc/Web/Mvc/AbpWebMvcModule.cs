using Abp.Web.Mvc.Web.Mvc.Configuration;
using Abp.Web.Mvc.Web.Mvc.Controllers;
using Abp.Web.Mvc.Web.Mvc.Security.AntiForgery;
using AbpFramework.Modules;
using System.Reflection;
using System.Web.Mvc;

namespace Abp.Web.Mvc.Web.Mvc
{
    /// <summary>
    /// 用于使用Abp构建ASP.NET MVC网站模块。    
    /// </summary>
    //[DependsOn(typeof(AbpWebModule))]
    public class AbpWebMvcModule : AbpModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<IAbpMvcConfiguration, AbpMvcConfiguration>();
        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            //设置指定的Controller的工厂，以替代系统默认的工厂
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(IocManager));
        }
        public override void PostInitialize()
        {
            GlobalFilters.Filters.Add(IocManager.Resolve<AbpAntiForgeryMvcFilter>());
        }
    }
}
