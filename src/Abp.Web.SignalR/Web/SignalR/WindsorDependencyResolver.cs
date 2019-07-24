using Castle.Windsor;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Abp.Web.SignalR.Web.SignalR
{
    public class WindsorDependencyResolver: DefaultDependencyResolver
    {
        private readonly IWindsorContainer _windsorContainer;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="windsorContainer"></param>
        public WindsorDependencyResolver(IWindsorContainer windsorContainer)
        {
            _windsorContainer = windsorContainer;
        }
        public override object GetService(Type serviceType)
        {
            return _windsorContainer.Kernel.HasComponent(serviceType)
                ? _windsorContainer.Resolve(serviceType)
                : base.GetService(serviceType);
        }
        public override IEnumerable<object> GetServices(Type serviceType)
        {
            return _windsorContainer.Kernel.HasComponent(serviceType)
                ? _windsorContainer.ResolveAll(serviceType).Cast<object>()
                : base.GetServices(serviceType);
        }
    }
}
