using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Configuration.Startup
{
    public class MultiTenancyConfig : IMultiTenancyConfig
    {
        public bool IsEnabled { get; set; }
    }
}
