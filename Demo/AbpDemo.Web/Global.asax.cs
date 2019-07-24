using Abp.Castle.Logging.Log4Net;
using Abp.Web;
using AbpDemo.Web.App_Start;
using AbpFramework;
using Castle.Facilities.Logging;
using System;
namespace AbpDemo.Web
{
    public class MvcApplication : AbpWebApplication<ABPMultiMVCWebModule>
    {
        //private Castle.Windsor.IWindsorContainer _container;
        protected override void Application_Start(object sender, EventArgs e)
        {
            AbpBootstrapper.IocManager.IocContainer.AddFacility<LoggingFacility>(
                f => f.UseAbpLog4Net().WithConfig(Server.MapPath("log4net.config"))
                );
            base.Application_Start(sender, e);
            //ABPMultiMVCWebModule aBPMultiMVCWebModule = new ABPMultiMVCWebModule();
            //AbpKernelModule abpKernelModule = new AbpKernelModule();
            //abpKernelModule.PreInitialize();
            //aBPMultiMVCWebModule.PreInitialize();
            //abpKernelModule.Initialize();
            //aBPMultiMVCWebModule.Initialize();
            //abpKernelModule.PostInitialize();
            //aBPMultiMVCWebModule.PostInitialize();
        }
    }
}
