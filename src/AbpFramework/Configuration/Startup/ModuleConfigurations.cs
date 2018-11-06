using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Configuration.Startup
{
    public class ModuleConfigurations : IModuleConfigurations
    {
        public IAbpStartupConfiguration AbpConfiguration { get; private set; }
      
        public ModuleConfigurations(IAbpStartupConfiguration abpStartupConfiguration)
        {
            AbpConfiguration = abpStartupConfiguration;
        }
    }
}
