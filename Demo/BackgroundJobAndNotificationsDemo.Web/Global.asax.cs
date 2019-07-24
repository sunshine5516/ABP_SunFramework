using Abp.Web;
using Castle.Facilities.Logging;
using System;
using Abp.Castle.Logging.Log4Net;
namespace BackgroundJobAndNotificationsDemo.Web
{
    public class MvcApplication : AbpWebApplication<BackgroundJobAndNotificationsDemoWebModule>
    {
        protected override void Application_Start(object sender, EventArgs e)
        {
            AbpBootstrapper.IocManager.IocContainer.AddFacility<LoggingFacility>(
               f => f.UseAbpLog4Net().WithConfig(Server.MapPath("log4net.config"))
               );
            base.Application_Start(sender, e);
        }
    }
}
