using Abp.WebApi.Configuration;
using Abp.WebApi.Controllers.Dynamic.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;

namespace Abp.WebApi.Controllers.Dynamic.Selectors
{
    /// <summary>
    /// 继承自ASP.Net WebAPI 的 ApiControllerActionSelector。 
    /// 通过重写SelectAction来返回HttpActionDescriptor的派生类
    /// DynamicHttpActionDescriptor的实例， 这是ABP能执行动态创建的APIController的Action方法的关键。
    /// </summary>
    public class AbpApiControllerActionSelector : ApiControllerActionSelector
    {
        private readonly IAbpWebApiConfiguration _configuration;

        public AbpApiControllerActionSelector(IAbpWebApiConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// This class is called by Web API system to select action method from given controller.
        /// </summary>
        /// <param name="controllerContext">Controller context</param>
        /// <returns>Action to be used</returns>
        public override HttpActionDescriptor SelectAction(HttpControllerContext controllerContext)
        {
            object controllerInfoObj;
            if (!controllerContext.ControllerDescriptor.Properties.TryGetValue("__AbpDynamicApiControllerInfo", out controllerInfoObj))
            {
                return GetDefaultActionDescriptor(controllerContext);
            }

            //Get controller information which is selected by AbpHttpControllerSelector.
            var controllerInfo = controllerInfoObj as DynamicApiControllerInfo;
            if (controllerInfo == null)
            {
                throw new Exception("__AbpDynamicApiControllerInfo in ControllerDescriptor.Properties is not a " + typeof(DynamicApiControllerInfo).FullName + " class.");
            }

            //No action name case
            var hasActionName = (bool)controllerContext.ControllerDescriptor.Properties["__AbpDynamicApiHasActionName"];
            if (!hasActionName)
            {
                return GetActionDescriptorByCurrentHttpVerb(controllerContext, controllerInfo);
            }

            //Get action name from route
            var serviceNameWithAction = (controllerContext.RouteData.Values["serviceNameWithAction"] as string);
            if (serviceNameWithAction == null)
            {
                return GetDefaultActionDescriptor(controllerContext);
            }

            var actionName = DynamicApiServiceNameHelper.GetActionNameInServiceNameWithAction(serviceNameWithAction);

            return GetActionDescriptorByActionName(
                controllerContext,
                controllerInfo,
                actionName
                );
        }

        private HttpActionDescriptor GetActionDescriptorByCurrentHttpVerb(HttpControllerContext controllerContext, DynamicApiControllerInfo controllerInfo)
        {
            //Check if there is only one action with the current http verb
            var actionsByVerb = controllerInfo.Actions.Values
                .Where(action => action.Verb == controllerContext.Request.Method.ToHttpVerb())
                .ToArray();

            if (actionsByVerb.Length == 0)
            {
                throw new HttpException(
                    (int)HttpStatusCode.NotFound,
                    "There is no action" +
                    " defined for api controller " + controllerInfo.ServiceName +
                    " with an http verb: " + controllerContext.Request.Method
                );
            }

            if (actionsByVerb.Length > 1)
            {
                throw new HttpException(
                    (int)HttpStatusCode.InternalServerError,
                    "There are more than one action" +
                    " defined for api controller " + controllerInfo.ServiceName +
                    " with an http verb: " + controllerContext.Request.Method
                );
            }

            //Return the single action by the current http verb
            return new DynamicHttpActionDescriptor(_configuration, controllerContext.ControllerDescriptor, actionsByVerb[0]);
        }

        private HttpActionDescriptor GetActionDescriptorByActionName(HttpControllerContext controllerContext, DynamicApiControllerInfo controllerInfo, string actionName)
        {
            //Get action information by action name
            DynamicApiActionInfo actionInfo;
            if (!controllerInfo.Actions.TryGetValue(actionName, out actionInfo))
            {
                throw new Exception("There is no action " + actionName + " defined for api controller " + controllerInfo.ServiceName);
            }

            if (actionInfo.Verb != controllerContext.Request.Method.ToHttpVerb())
            {
                throw new HttpException(
                    (int)HttpStatusCode.BadRequest,
                    "There is an action " + actionName +
                    " defined for api controller " + controllerInfo.ServiceName +
                    " but with a different HTTP Verb. Request verb is " + controllerContext.Request.Method +
                    ". It should be " + actionInfo.Verb
                );
            }

            return new DynamicHttpActionDescriptor(_configuration, controllerContext.ControllerDescriptor, actionInfo);
        }

        private HttpActionDescriptor GetDefaultActionDescriptor(HttpControllerContext controllerContext)
        {
            return base.SelectAction(controllerContext);
        }
    }
}
