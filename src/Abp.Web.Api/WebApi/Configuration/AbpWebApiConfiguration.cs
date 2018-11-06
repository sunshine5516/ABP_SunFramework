using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Abp.WebApi.Controllers.Dynamic.Builders;

namespace Abp.WebApi.Configuration
{
    /// <summary>
    /// 封装了HttpConfiguration属性
    /// </summary>
    public class AbpWebApiConfiguration : IAbpWebApiConfiguration
    {
        public HttpConfiguration HttpConfiguration { get; set; }
        public bool IsValidationEnabledForControllers { get; set; }
        public bool IsAutomaticAntiForgeryValidationEnabled { get; set; }
        public bool SetNoCacheForAllResponses { get; set; }

        public IDynamicApiControllerBuilder DynamicApiControllerBuilder { get; }
        public AbpWebApiConfiguration(IDynamicApiControllerBuilder dynamicApiControllerBuilder)
        {
            DynamicApiControllerBuilder = dynamicApiControllerBuilder;

            HttpConfiguration = GlobalConfiguration.Configuration;
            IsValidationEnabledForControllers = true;
            IsAutomaticAntiForgeryValidationEnabled = true;
            SetNoCacheForAllResponses = true;
        }
    }
}
