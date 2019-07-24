using AbpFramework.Configuration;
using AbpFramework.Extensions;
using AbpFramework.Dependency;
using System.Web;
using System.Web.Mvc;
namespace Abp.Web.Mvc.Web.Mvc.Views
{
    /// <summary>
    /// Abp view基类
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public abstract class AbpWebViewPage<TModel>: WebViewPage<TModel>
    {
        public string ApplicationPath
        {
            get
            {
                var appPath = HttpContext.Current.Request.ApplicationPath;
                if (appPath == null)
                {
                    return "/";
                }

                appPath = appPath.EnsureEndsWith('/');

                return appPath;
            }
        }
        /// <summary>
        /// SettingManager实例.
        /// </summary>
        public ISettingManager SettingManager { get; set; }
        protected AbpWebViewPage()
        {
            SettingManager= SingletonDependency<ISettingManager>.Instance;
        }
    }
    

}
