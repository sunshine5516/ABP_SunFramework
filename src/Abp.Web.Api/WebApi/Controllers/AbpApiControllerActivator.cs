using AbpFramework.Dependency;
using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Abp.WebApi.Controllers
{
    /// <summary>
    /// 实现了IHttpControllerActivator接口，根据controllerType生成具体的controller.
    /// </summary>
    public class AbpApiControllerActivator : IHttpControllerActivator
    {
        private readonly IIocResolver _iocResolver;
        public AbpApiControllerActivator(IIocResolver iocResolver)
        {
            _iocResolver = iocResolver;
        }
        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var controllerWrapper = _iocResolver.ResolveAsDisposable<IHttpController>(controllerType);
            request.RegisterForDispose(controllerWrapper);
            return controllerWrapper.Object;
        }
    }
}
