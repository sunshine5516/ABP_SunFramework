using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Abp.WebApi.Controllers.Dynamic.Builders
{
    /// <summary>
    /// 用于批量定义动态API控制器
    /// </summary>
    public interface IBatchApiControllerBuilder<T>
    {
        /// <summary>
        /// 用于过滤类型
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IBatchApiControllerBuilder<T> Where(Func<Type, bool> predicate);
        /// <summary>
        /// 添加过滤器
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        IBatchApiControllerBuilder<T> WithFilters(params IFilter[] filters);
        /// <summary>
        /// Enables/Disables API Explorer for dynamic controllers.
        /// </summary>
        IBatchApiControllerBuilder<T> WithApiExplorer(bool isEnabled);
        /// <summary>
        /// 代理脚本
        /// 默认enabled
        /// </summary>
        IBatchApiControllerBuilder<T> WithProxyScripts(bool isEnabled);
        /// <summary>
        /// 设置服务名称
        /// </summary>
        /// <param name="serviceNameSelector"></param>
        /// <returns></returns>
        IBatchApiControllerBuilder<T> WithServiceName(Func<Type, string> serviceNameSelector);
        /// <summary>
        /// Used to perform actions for each method of all dynamic api controllers.
        /// </summary>
        /// <param name="action">方法.</param>
        /// <returns>The current Controller Builder</returns>
        IBatchApiControllerBuilder<T> ForMethods(Action<IApiControllerActionBuilder> action);
        /// <summary>
        /// 请求方式
        /// 默认POST
        /// </summary>
        /// <returns>当前控制器构建器</returns>
        IBatchApiControllerBuilder<T> WithConventionalVerbs();
        /// <summary>
        /// 构建controller.
        /// 必须在构建操作的最后调用此方法。
        /// </summary>
        void Build();
    }
}
