using Abp.Web.Mvc.Web.Mvc.Configuration;
using Abp.Web.Mvc.Web.Mvc.Controllers.Results;
using Abp.Web.Mvc.Web.Mvc.Extensions;
using Abp.Web.Mvc.Web.Mvc.Helpers;
using AbpFramework;
using AbpFramework.Configuration;
using AbpFramework.Domain.Entities;
using AbpFramework.Domain.Uow;
using AbpFramework.Events.Bus;
using AbpFramework.Events.Bus.Exceptions;
using AbpFramework.Logging;
using AbpFramework.ObjectMapping;
using AbpFramework.Reflection;
using AbpFramework.Runtime.Session;
using AbpFramework.Runtime.Validation;
using AbpFramework.Web.Models;
using Castle.Core.Logging;
using System;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
namespace Abp.Web.Mvc.Web.Mvc.Controllers
{
    public abstract class AbpController : Controller
    {
        #region 声明实例
        public IAbpSession AbpSession { get; set; }
        protected string LocalizationSourceName { get; set; }
        public ILogger Logger { get; set; }
        public IObjectMapper ObjectMapper { get; set; }
        private IUnitOfWorkManager _unitOfWorkManager;
        public ISettingManager SettingManager { get; set; }
        public IEventBus EventBus { get; set; }
        public IUnitOfWorkManager UnitOfWorkManager
        {
            get
            {
                if(_unitOfWorkManager==null)
                {
                    throw new AbpException("Must set UnitOfWorkManager before use it.");
                }
                return _unitOfWorkManager;
            }
            set { _unitOfWorkManager = value; }
        }
        /// <summary>
        /// 获取当前UOW
        /// </summary>
        protected IActiveUnitOfWork CurrentUnitOfWork { get { return UnitOfWorkManager.Current; } }
        public IAbpMvcConfiguration AbpMvcConfiguration { get; set; }
        /// <summary>
        /// 当前正在执行的操作的信息
        /// </summary>
        private MethodInfo _currentMethodInfo;
        private WrapResultAttribute _wrapResultAttribute;
        #endregion
        #region 构造函数
        protected AbpController()
        {
            AbpSession = NullAbpSession.Instance;
            Logger = NullLogger.Instance;
            ObjectMapper = NullObjectMapper.Instance;
            EventBus = NullEventBus.Instance;
        }
        #endregion
        #region 方法
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            if (_wrapResultAttribute != null && !_wrapResultAttribute.WrapOnSuccess)
            {
                return base.Json(data, contentType, contentEncoding, behavior);
            }
            return AbpJson(data, contentType, contentEncoding, behavior);
        }
        protected virtual AbpJsonResult AbpJson(
            object data,
            string contentType = null,
            Encoding contentEncoding = null,
            JsonRequestBehavior behavior = JsonRequestBehavior.DenyGet,
            bool wrapResult = true,
            bool camelCase = true,
            bool indented = false)
        {
            if(wrapResult)
            {
                //if(data==null)
                //{
                //    data = new AjaxResponse();
                //}
                //else if (!(data is AjaxResponseBase))
                //{
                //    data = new AjaxResponse(data);
                //}
            }
            return new AbpJsonResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                CamelCase = camelCase,
                Indented = indented
            };
        }
        #endregion
        #region OnActionExecuting / OnActionExecuted
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            SetCurrentMethodInfoAndWrapResultAttribute(filterContext);
            base.OnActionExecuting(filterContext);
        }

        private void SetCurrentMethodInfoAndWrapResultAttribute(ActionExecutingContext filterContext)
        {
            if (_currentMethodInfo != null)
            {
                return;
            }
            _currentMethodInfo = filterContext.ActionDescriptor.GetMethodInfoOrNull();
            _wrapResultAttribute = ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault
                (_currentMethodInfo, AbpMvcConfiguration.DefaultWrapResultAttribute);
        }
        #endregion
        #region 异常处理
        protected override void OnException(ExceptionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            //如果异常已经处理，则什么都不做
            //如果这是子操作，则应由主操作处理异常。
            if(context.ExceptionHandled||context.IsChildAction)
            {
                base.OnException(context);
                return;
            }
            if(_wrapResultAttribute==null||_wrapResultAttribute.LogError)
            {
                LogHelper.LogException(Logger, context.Exception);
            }
            if(!context.HttpContext.IsCustomErrorEnabled)
            {
                base.OnException(context);
                return;
            }
            if(new HttpException(null,context.Exception).GetHashCode()!=500)
            {
                base.OnException(context);
                return;
            }
            if(_wrapResultAttribute==null||!_wrapResultAttribute.WrapOnError)
            {
                base.OnException(context);
                return;
            }
            context.ExceptionHandled = true;
            context.HttpContext.Response.Clear();
            context.HttpContext.Response.StatusCode= GetStatusCodeForException(context);
            context.Result= MethodInfoHelper.IsJsonResult(_currentMethodInfo)
                ? GenerateJsonExceptionResult(context)
                : GenerateNonJsonExceptionResult(context);
            context.HttpContext.Response.TrySkipIisCustomErrors = true;
            EventBus.Trigger(this, new AbpHandledExceptionData(context.Exception));
            //base.OnException(context);
        }

        protected virtual ActionResult GenerateNonJsonExceptionResult(ExceptionContext context)
        {
            return new ViewResult
            {
                ViewName = "Error",
                MasterName = string.Empty,
                //ViewData = new ViewDataDictionary<ErrorViewModel>(new ErrorViewModel(ErrorInfoBuilder.BuildForException(context.Exception), context.Exception)),
                TempData = context.Controller.TempData
            };
        }

        protected virtual ActionResult GenerateJsonExceptionResult(ExceptionContext context)
        {
            context.HttpContext.Items.Add("IgnoreJsonRequestBehaviorDenyGet", "true");
            return new AbpJsonResult(
                //new AjaxRespone(
                    
                //    )
                );
        }

        protected virtual int GetStatusCodeForException(ExceptionContext context)
        {
            //if(context.Exception is AbpAuthorizationException)
            //{
            //    return context.HttpContext.User.Identity.IsAuthenticated
            //        ? (int)HttpStatusCode.Forbidden
            //        : (int)HttpStatusCode.Unauthorized;
            //}
            if(context.Exception is AbpValidationException)
            {
                return (int)HttpStatusCode.BadRequest;
            }
            if(context.Exception is EntityNotFoundException)
            {
                return (int)HttpStatusCode.NotFound;
            }
            return (int)HttpStatusCode.InternalServerError;            
        }
        #endregion
    }
}
