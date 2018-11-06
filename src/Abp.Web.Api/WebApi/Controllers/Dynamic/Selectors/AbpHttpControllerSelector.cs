using Abp.WebApi.Controllers.Dynamic.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using AbpFramework.Collections.Extensions;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Abp.WebApi.Controllers.Dynamic.Selectors
{
    /// <summary>
    /// 继承自asp.net Webapi系统的DefaultHttpControllerSelector。
    /// 通过重写SelectController来返回HttpControllerDescriptor，
    /// 这是ABP能动态创建APIController的关键
    /// </summary>
    public class AbpHttpControllerSelector : DefaultHttpControllerSelector
    {
        private readonly HttpConfiguration _configuration;
        private readonly DynamicApiControllerManager _dynamicApiControllerManager;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration"></param>
        public AbpHttpControllerSelector(HttpConfiguration configuration, DynamicApiControllerManager dynamicApiControllerManager)
             : base(configuration)
        {
            _configuration = configuration;
            _dynamicApiControllerManager = dynamicApiControllerManager;
        }
        /// <summary>
        /// Web API系统调用此方法为此请求选择控制器
        /// </summary>
        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            if (request == null)
            {
                return base.SelectController(null);
            }
            var routeData = request.GetRouteData();
            if (routeData == null)
            {
                return base.SelectController(request);
            }
            //Get serviceNameWithAction from route
            string serviceNameWithAction;
            if (!routeData.Values.TryGetValue("serviceNameWithAction", out serviceNameWithAction))
            {
                return base.SelectController(request);
            }
            if (serviceNameWithAction.EndsWith("/"))
            {
                serviceNameWithAction = serviceNameWithAction.Substring(0, serviceNameWithAction.Length - 1);
                routeData.Values["serviceNameWithAction"] = serviceNameWithAction;
            }
            var hasActionName = false;
            var controllerInfo = _dynamicApiControllerManager.FindOrNull(serviceNameWithAction);
            if (controllerInfo == null)
            {
                if (!DynamicApiServiceNameHelper.IsValidServiceNameWithAction(serviceNameWithAction))
                {
                    return base.SelectController(request);
                }
                var serviceName = DynamicApiServiceNameHelper.GetServiceNameInServiceNameWithAction(serviceNameWithAction);
                controllerInfo = _dynamicApiControllerManager.FindOrNull(serviceName);
                if (controllerInfo == null)
                {
                    return base.SelectController(request);
                }
                hasActionName = true;
            }
            var controllerDescriptor = new DynamicHttpControllerDescriptor(_configuration, controllerInfo);
            controllerDescriptor.Properties["__AbpDynamicApiControllerInfo"] = controllerInfo;
            controllerDescriptor.Properties["__AbpDynamicApiHasActionName"] = hasActionName;
            return controllerDescriptor;

        }
    }
}
