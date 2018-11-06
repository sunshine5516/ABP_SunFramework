using AbpFramework.Dependency;
using AbpFramework.Runtime.Validation.Interception;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Abp.Web.Mvc.Web.Mvc.Controllers
{
    /// <summary>
    /// 重写控制器依赖注入工厂
    /// </summary>
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IIocResolver _iocManager;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iocManager">Reference to DI kernel</param>
        public WindsorControllerFactory(IIocResolver iocManager)
        {
            _iocManager = iocManager;
        }

        /// <summary>
        /// 释放给定的实例.
        /// </summary>
        /// <param name="controller">Controller instance</param>
        public override void ReleaseController(IController controller)
        {
            _iocManager.Release(controller);
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="requestContext">Request context</param>
        /// <param name="controllerType">Controller type</param>
        /// <returns></returns>
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                return base.GetControllerInstance(requestContext, controllerType);
            }
            //return (IController)_iocManager.Resolve(controllerType);
            return _iocManager.Resolve<IController>(controllerType);
        }
        //public override IController CreateController(System.Web.Routing.RequestContext requestContext, string controllerName)
        //{
        //    var controllerComponentName = controllerName + "Controller";
        //    return _iocManager.Resolve<IController>(controllerComponentName);
        //}
    }
}
