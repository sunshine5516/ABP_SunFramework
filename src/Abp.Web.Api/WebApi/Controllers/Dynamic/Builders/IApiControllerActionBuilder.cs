using Abp.Web.Common.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Abp.WebApi.Controllers.Dynamic.Builders
{
    public interface IApiControllerActionBuilder
    {
        /// <summary>
        /// 关联到此action的controller
        /// </summary>
        IApiControllerBuilder Controller { get; }
        /// <summary>
        /// action名称.
        /// </summary>
        string ActionName { get; }
        /// <summary>
        /// Gets the action method.
        /// </summary>
        MethodInfo Method { get; }

        /// <summary>
        /// 当前的Http Verb设置.
        /// </summary>
        HttpVerb? Verb { get; set; }

        /// <summary>
        /// Is API Explorer enabled.
        /// </summary>
        bool? IsApiExplorerEnabled { get; set; }

        /// <summary>
        /// 过滤器
        /// </summary>
        IFilter[] Filters { get; set; }

        /// <summary>
        /// 是否创建方法.
        /// </summary>
        bool DontCreate { get; set; }
    }
    public interface IApiControllerActionBuilder<T> : IApiControllerActionBuilder
    {
        IApiControllerActionBuilder<T> WithVerb(HttpVerb verb);

        IApiControllerActionBuilder<T> WithApiExplorer(bool isEnabled);
        IApiControllerActionBuilder<T> ForMethod(string methodName);
        /// <summary>
        /// 不创建动作
        /// </summary>
        /// <returns></returns>
        IApiControllerBuilder<T> DontCreateAction();
        /// <summary>
        /// 创建控制器.
        /// This method must be called at last of the build operation.
        /// </summary>
        void Build();
        /// <summary>
        /// 过滤器
        /// </summary>
        /// <param name="filters"> Action Filters to apply.</param>
        IApiControllerActionBuilder<T> WithFilters(params IFilter[] filters);
    }
}
