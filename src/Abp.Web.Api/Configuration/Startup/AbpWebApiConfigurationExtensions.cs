using Abp.WebApi.Configuration;
using AbpFramework.Configuration.Startup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.Configuration.Startup
{
    public static class AbpWebApiConfigurationExtensions
    {
        public static IAbpWebApiConfiguration AbpWebApi(this IModuleConfigurations configurations)
        {
            return configurations.AbpConfiguration.Get<IAbpWebApiConfiguration>();
        }
    }
}
