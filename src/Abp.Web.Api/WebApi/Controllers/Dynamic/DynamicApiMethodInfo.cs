using Abp.Web.Common.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Abp.WebApi.Controllers.Dynamic
{
    class DynamicApiMethodInfo
    {
    }
    /// <summary>
    /// 封装动态生成的ApiController的Action的信息
    /// </summary>
    public class DynamicApiActionInfo
    {
        /// <summary>
        /// action 名称
        /// </summary>
        public string ActionName { get; private set; }
        /// <summary>
        /// 方法信息
        /// </summary>
        public MethodInfo Method { get; private set; }
        public HttpVerb Verb { get; private set; }
        /// <summary>
        /// 过滤器
        /// </summary>
        public IFilter[] Filters { get; set; }
        /// <summary>
        /// Is API Explorer enabled.
        /// </summary>
        public bool? IsApiExplorerEnabled { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="verb"></param>
        /// <param name="method"></param>
        /// <param name="filters"></param>
        /// <param name="isApiExplorerEnabled"></param>
        public DynamicApiActionInfo(
        string actionName,
        HttpVerb verb,
        MethodInfo method,
        IFilter[] filters = null,
        bool? isApiExplorerEnabled = null)
        {
            ActionName = actionName;
            Verb = verb;
            Method = method;
            IsApiExplorerEnabled = isApiExplorerEnabled;
            Filters = filters ?? new IFilter[] { }; //Assigning or initialzing the action filters.
        }
    }
}
