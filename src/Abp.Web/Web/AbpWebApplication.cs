using AbpFramework;
using AbpFramework.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Abp.Web
{
    /// <summary>
    /// 该类用于简化ABP系统的启动，使用<see cref ="AbpBootstrapper"/> class
    /// 在global.asax中继承此类而不是<see cref ="HttpApplication"/>以启动ABP系统。
    /// </summary>
    /// <typeparam name="TStartupModule">依赖于其他使用模块的应用程序的启动模块。 应该从<see cref ="AbpModule"/>中派生.</typeparam>
    public abstract class AbpWebApplication<TStartupModule> : HttpApplication
        where TStartupModule : AbpModule
    {
        public static AbpBootstrapper AbpBootstrapper { get; } = AbpBootstrapper.Create<TStartupModule>();
        protected virtual void Application_Start(object sender, EventArgs e)
        {
            AbpBootstrapper.Initialize();
        }
        protected virtual void Application_End(object sender, EventArgs e)
        {
            AbpBootstrapper.Dispose();
        }

        protected virtual void Session_Start(object sender, EventArgs e)
        {

        }

        protected virtual void Session_End(object sender, EventArgs e)
        {

        }

        protected virtual void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected virtual void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected virtual void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            SetCurrentCulture();
        }

        protected virtual void Application_EndRequest(object sender, EventArgs e)
        {

        }

        protected virtual void Application_Error(object sender, EventArgs e)
        {

        }

        protected virtual void SetCurrentCulture()
        {
            //AbpBootstrapper.IocManager.Using<ICurrentCultureSetter>(cultureSetter => cultureSetter.SetCurrentCulture(Context));
        }

    }
}
