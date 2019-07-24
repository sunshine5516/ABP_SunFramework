using System;
using System.Configuration;
using BackgroundJobAndNotificationsDemo.Web;
using BackgroundJobAndNotificationsDemo.Web.Controllers;
using Hangfire;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace BackgroundJobAndNotificationsDemo.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.UseAbp();

            //app.UseOAuthBearerAuthentication(AccountController.OAuthBearerOptions);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);


            app.MapSignalR();

            app.UseHangfireDashboard(); //Enable hangfire dashboard.
        }

      
        private static bool IsTrue(string appSettingName)
        {
            return string.Equals(
                ConfigurationManager.AppSettings[appSettingName],
                "true",
                StringComparison.InvariantCultureIgnoreCase);
        }
    }
}