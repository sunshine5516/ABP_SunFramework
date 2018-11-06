using Abp.WebApi.Controllers.Dynamic.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Abp.WebApi.Configuration
{
    /// <summary>
    /// WebApi配置模块
    /// </summary>
    public interface IAbpWebApiConfiguration
    {
        HttpConfiguration HttpConfiguration { get; set; }
        /// <summary>
        /// 默认: true.
        /// </summary>
        bool IsValidationEnabledForControllers { get; set; }
        /// <summary>
        /// 默认: true.
        /// </summary>
        bool IsAutomaticAntiForgeryValidationEnabled { get; set; }
        /// <summary>
        /// 默认: true.
        /// </summary>
        bool SetNoCacheForAllResponses { get; set; }
        /// <summary>
        /// 配置动态Web API 控制器
        /// </summary>
        IDynamicApiControllerBuilder DynamicApiControllerBuilder { get; }
    }
}
