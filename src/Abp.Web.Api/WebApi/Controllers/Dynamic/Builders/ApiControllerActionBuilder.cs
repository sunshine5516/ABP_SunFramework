using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;
using Abp.Web.Common.Web;
using Abp.WebApi.Controllers.Dynamic.Builders;
using AbpFramework.Dependency;
using AbpFramework.Extensions;
using AbpFramework.Reflection;
using AbpFramework.Threading;

namespace Abp.WebApi.Controllers.Dynamic.Builders
{
    internal class ApiControllerActionBuilder<T> : IApiControllerActionBuilder<T>
    {
        private readonly ApiControllerBuilder<T> _controller;
        private readonly IIocResolver _iocResolver;
        public IApiControllerBuilder Controller {
            get { return _controller; }
        }
        /// <summary>
        /// action名称
        /// </summary>
        public string ActionName { get; }
        /// <summary>
        /// 方法信息
        /// </summary>
        public MethodInfo Method { get; }
        /// <summary>
        /// 请求类型
        /// </summary>
        public HttpVerb? Verb { get; set; }
        public bool? IsApiExplorerEnabled { get; set; }
        /// <summary>
        /// 过滤器
        /// </summary>
        public IFilter[] Filters { get; set; }
        /// <summary>
        /// 是否创建
        /// </summary>
        public bool DontCreate { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="apiControllerBuilder"></param>
        /// <param name="methodInfo"></param>
        /// <param name="iocResolver"></param>
        public ApiControllerActionBuilder(ApiControllerBuilder<T> apiControllerBuilder, MethodInfo methodInfo, IIocResolver iocResolver)
        {
            _controller = apiControllerBuilder;
            _iocResolver = iocResolver;
            Method = methodInfo;
            ActionName = GetNormalizedActionName();

        }

        private string GetNormalizedActionName()
        {
            //using (var config = _iocResolver.ResolveAsDisposable<IApiProxyScriptingConfiguration>())
            //{
            //    if (!config.Object.RemoveAsyncPostfixOnProxyGeneration)
            //    {
            //        return Method.Name;
            //    }
            //}
            ///是否为异步方法
            if (!Method.IsAsync())
            {
                return Method.Name;
            }

            return Method.Name.RemovePostFix("Async");
        }

        public void Build()
        {
            _controller.Build();
        }

        public IApiControllerBuilder<T> DontCreateAction()
        {
            DontCreate = true;
            return _controller;
        }

        public IApiControllerActionBuilder<T> ForMethod(string methodName)
        {
            return _controller.ForMethod(methodName);
        }

        public IApiControllerActionBuilder<T> WithApiExplorer(bool isEnabled)
        {
            IsApiExplorerEnabled = isEnabled;
            return this;
        }

        public IApiControllerActionBuilder<T> WithFilters(params IFilter[] filters)
        {
            Filters = filters;
            return this;
        }

        public IApiControllerActionBuilder<T> WithVerb(HttpVerb verb)
        {
            Verb = verb;
            return this;
        }
        /// <summary>
        /// action基本信息
        /// </summary>
        /// <param name="conventionalVerbs"></param>
        /// <returns></returns>
        internal DynamicApiActionInfo BuildActionInfo(bool conventionalVerbs)
        {
            return new DynamicApiActionInfo(
                ActionName,
                GetNormalizedVerb(conventionalVerbs),
                Method,
                Filters,
                IsApiExplorerEnabled);
        }
        private HttpVerb GetNormalizedVerb(bool conventionalVerbs)
        {
            if (Verb != null)
            {
                return Verb.Value;
            }

            if (Method.IsDefined(typeof(HttpGetAttribute)))
            {
                return HttpVerb.Get;
            }

            if (Method.IsDefined(typeof(HttpPostAttribute)))
            {
                return HttpVerb.Post;
            }

            if (Method.IsDefined(typeof(HttpPutAttribute)))
            {
                return HttpVerb.Put;
            }

            if (Method.IsDefined(typeof(HttpDeleteAttribute)))
            {
                return HttpVerb.Delete;
            }

            if (Method.IsDefined(typeof(HttpPatchAttribute)))
            {
                return HttpVerb.Patch;
            }

            if (Method.IsDefined(typeof(HttpOptionsAttribute)))
            {
                return HttpVerb.Options;
            }

            if (Method.IsDefined(typeof(HttpHeadAttribute)))
            {
                return HttpVerb.Head;
            }

            if (conventionalVerbs)
            {
                var conventionalVerb = DynamicApiVerbHelper.GetConventionalVerbForMethodName(ActionName);
                if (conventionalVerb == HttpVerb.Get && !HasOnlyPrimitiveIncludingNullableTypeParameters(Method))
                {
                    conventionalVerb = DynamicApiVerbHelper.GetDefaultHttpVerb();
                }

                return conventionalVerb;
            }

            return DynamicApiVerbHelper.GetDefaultHttpVerb();
        }
        private static bool HasOnlyPrimitiveIncludingNullableTypeParameters(MethodInfo methodInfo)
        {
            return methodInfo.GetParameters().All(p => TypeHelper.IsPrimitiveExtendedIncludingNullable(p.ParameterType) || p.IsDefined(typeof(FromUriAttribute)));
        }
    }
}
