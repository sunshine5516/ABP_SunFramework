using Abp.Web.Mvc.Web.Mvc.Configuration;
using AbpFramework.Dependency;
using Castle.Core.Logging;
using System;
using System.Web.Mvc;
namespace Abp.Web.Mvc.Web.Mvc.Security.AntiForgery
{
    public class AbpAntiForgeryMvcFilter : IAuthorizationFilter, ITransientDependency
    {
        #region 声明实例
        public ILogger Logger { get; set; }
        private readonly IAbpMvcConfiguration _mvcConfiguration;
        #endregion
        #region 构造函数
        public AbpAntiForgeryMvcFilter(IAbpMvcConfiguration mvcConfiguration)
        {
            _mvcConfiguration = mvcConfiguration;
            Logger = NullLogger.Instance;
        }
        #endregion
        #region 方法
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            //throw new NotImplementedException();
        }
        #endregion

    }
}
