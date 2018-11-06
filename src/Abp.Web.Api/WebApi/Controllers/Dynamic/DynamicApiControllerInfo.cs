using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Abp.WebApi.Controllers.Dynamic
{
    /// <summary>
    /// 封装ApiController的信息.
    /// </summary>
    public class DynamicApiControllerInfo
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; private set; }
        /// <summary>
        /// 接口类型
        /// </summary>
        public Type ServiceInterfaceType { get; private set; }
        /// <summary>
        /// Api控制器类型
        /// </summary>
        public Type ApiControllerType { get; set; }
        /// <summary>
        /// 拦截器类型.
        /// </summary>
        public Type InterceptorType { get; private set; }
        /// <summary>
        /// Is API Explorer enabled.
        /// </summary>
        public bool? IsApiExplorerEnabled { get; private set; }
        /// <summary>
        /// 过滤器
        /// </summary>
        public IFilter[] Filters { get; set; }
        /// <summary>
        /// action集合
        /// </summary>
        public IDictionary<string, DynamicApiActionInfo> Actions { get; private set; }
        /// <summary>
        /// 代理脚本是否可运行
        /// </summary>
        public bool IsProxyScriptingEnabled { get; private set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        /// <param name="serviceInterfaceType">服务接口类型</param>
        /// <param name="apiControllerType">Api控制器类型</param>
        /// <param name="interceptorType">拦截器</param>
        /// <param name="filters">过滤器</param>
        /// <param name="isApiExplorerEnabled">Is API explorer enabled</param>
        /// <param name="isProxyScriptingEnabled">Is proxy scripting enabled</param>
        public DynamicApiControllerInfo(
            string serviceName,
            Type serviceInterfaceType,
            Type apiControllerType,
            Type interceptorType,
            IFilter[] filters = null,
            bool? isApiExplorerEnabled = null,
            bool isProxyScriptingEnabled = true)
        {
            ServiceName = serviceName;
            ServiceInterfaceType = serviceInterfaceType;
            ApiControllerType = apiControllerType;
            InterceptorType = interceptorType;
            IsApiExplorerEnabled = isApiExplorerEnabled;
            IsProxyScriptingEnabled = isProxyScriptingEnabled;
            Filters = filters ?? new IFilter[] { }; //Assigning or initialzing the action filters.
            Actions = new Dictionary<string, DynamicApiActionInfo>(StringComparer.InvariantCultureIgnoreCase);
        }

    }
}
