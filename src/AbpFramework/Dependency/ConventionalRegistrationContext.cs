using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Dependency
{
    public class ConventionalRegistrationContext : IConventionalRegistrationContext
    {
        public Assembly Assembly { get; private set; }

        public IIocManager IocManager { get; private set; }

        public ConventionalRegistrationConfig Config { get; private set; }
        internal ConventionalRegistrationContext(Assembly assembly, IIocManager iocManager, ConventionalRegistrationConfig config)
        {
            Assembly = assembly;
            IocManager = iocManager;
            Config = config;
        }
    }
}
