using Abp.WebApi.Controllers.Dynamic.Builders;
using AbpFramework.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using AbpFramework.Reflection.Extensions;
using AbpFramework.Application.Services;
using Abp.WebApi.Controllers.Dynamic.Interceptors;

namespace Abp.WebApi.Controllers.Dynamic.Builders
{
    internal class ApiControllerBuilder<T> : IApiControllerBuilder<T>
    {
        /// <summary>
        /// controller名称
        /// </summary>
        public string ServiceName { get; }
        /// <summary>
        /// 获取此动态控制器的服务接口的类型
        /// </summary>
        public Type ServiceInterfaceType { get; }

        public IFilter[] Filters { get; set; }
        public bool? IsApiExplorerEnabled { get; set; }
        public bool IsProxyScriptingEnabled { get; set; } = true;
        public bool ConventionalVerbs { get; set; }
        private readonly IDictionary<string, ApiControllerActionBuilder<T>> _actionBuilders;
        private readonly IIocResolver _iocResolver;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="iocResolver"></param>
        public ApiControllerBuilder(string serviceName, IIocResolver iocResolver)
        {
            if (string.IsNullOrWhiteSpace(serviceName))
            {
                throw new ArgumentException("serviceName null or empty!", "serviceName");
            }
            if (!DynamicApiServiceNameHelper.IsValidServiceName(serviceName))
            {
                throw new ArgumentException("serviceName is not properly formatted! It must contain a single-depth namespace at least! For example: 'myapplication/myservice'.", "serviceName");
            }
            _iocResolver = iocResolver;

            ServiceName = serviceName;
            ServiceInterfaceType = typeof(T);
            _actionBuilders = new Dictionary<string, ApiControllerActionBuilder<T>>();
            foreach (var methodInfo in DynamicApiControllerActionHelper.GetMethodsOfType(typeof(T)))
            {
                var actionBuilder = new ApiControllerActionBuilder<T>(this, methodInfo, iocResolver);
                var remoteServiceAttr = methodInfo.GetSingleAttributeOrNull<RemoteServiceAttribute>();
                if (remoteServiceAttr != null && !remoteServiceAttr.IsEnabledFor(methodInfo))
                {
                    actionBuilder.DontCreateAction();
                }
                _actionBuilders[methodInfo.Name] = actionBuilder;
            }
        }
        public IApiControllerActionBuilder<T> ForMethod(string methodName)
        {
            if (!_actionBuilders.ContainsKey(methodName))
            {
                throw new ArgumentException("There is no method with name " + methodName + " in type " + typeof(T).Name);
            }

            return _actionBuilders[methodName];
        }
        public IApiControllerBuilder<T> ForMethods(Action<IApiControllerActionBuilder> action)
        {
            foreach (var actionBuilder in _actionBuilders.Values)
            {
                action(actionBuilder);
            }

            return this;
        }
        public IApiControllerBuilder<T> WithConventionalVerbs()
        {
            ConventionalVerbs = true;
            return this;
        }

        public IApiControllerBuilder<T> WithApiExplorer(bool isEnabled)
        {
            IsApiExplorerEnabled = isEnabled;
            return this;
        }

        public IApiControllerBuilder<T> WithProxyScripts(bool isEnabled)
        {
            IsProxyScriptingEnabled = isEnabled;
            return this;
        }

        public IApiControllerBuilder<T> WithFilters(params IFilter[] filters)
        {
            Filters = filters;
            return this;
        }
        /// <summary>
        /// 构建控制器
        /// This method must be called at last of the build operation.
        /// </summary>
        public void Build()
        {
            var controllerInfo = new DynamicApiControllerInfo(
                ServiceName,
                ServiceInterfaceType,
                 typeof(DynamicApiController<T>),
                typeof(AbpDynamicApiControllerInterceptor<T>),
                Filters,
                IsApiExplorerEnabled,
                IsProxyScriptingEnabled
                );
            foreach (var actionBuilder in _actionBuilders.Values)
            {
                if (actionBuilder.DontCreate)
                {
                    continue;
                }
                controllerInfo.Actions[actionBuilder.ActionName] = actionBuilder.BuildActionInfo(ConventionalVerbs);

            }
            _iocResolver.Resolve<DynamicApiControllerManager>().Register(controllerInfo);
        }
    }
}
