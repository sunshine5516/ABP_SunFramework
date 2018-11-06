using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Abp.WebApi.Controllers.Dynamic.Builders
{
    public interface IApiControllerBuilder
    {
        string ServiceName { get; }
        /// <summary>
        /// 获取此动态控制器的服务接口的类型    
        /// </summary>
        Type ServiceInterfaceType { get; }
        /// <summary>
        /// 过滤器
        /// </summary>
        IFilter[] Filters { get; set; }
        /// <summary>
        /// Is API Explorer enabled.
        /// </summary>
        bool? IsApiExplorerEnabled { get; set; }
    }
    /// <summary>
    /// 该接口用于定义动态api控制器。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IApiControllerBuilder<T> : IApiControllerBuilder
    {
        /// <summary>
        /// 为动态控制器添加动作过滤器.
        /// </summary>
        /// <param name="filters"> 过滤器. </param>
        /// <returns>The current Controller Builder </returns>
        IApiControllerBuilder<T> WithFilters(params IFilter[] filters);
        IApiControllerActionBuilder<T> ForMethod(string methodName);
    }
}
