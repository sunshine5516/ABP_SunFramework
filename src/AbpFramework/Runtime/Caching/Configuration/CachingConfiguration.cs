using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbpFramework.Configuration.Startup;

namespace AbpFramework.Runtime.Caching.Configuration
{
    public class CachingConfiguration : ICachingConfiguration
    {
        #region 声明实例
        public IAbpStartupConfiguration AbpConfiguration { get; private set; }
        private readonly List<ICacheConfigurator> _configurators;

        public IReadOnlyList<ICacheConfigurator> Configurators
        {
            get { return _configurators.ToImmutableList(); }
        }

        #endregion
        #region 构造函数
        public CachingConfiguration(IAbpStartupConfiguration abpStartupConfiguration)
        {
            this.AbpConfiguration = abpStartupConfiguration;
            _configurators = new List<ICacheConfigurator>();
        }
        #endregion
        #region 方法
        public void Configure(string cacheName, Action<ICache> initAction)
        {
            _configurators.Add(new CacheConfigurator(cacheName, initAction));
        }

        public void ConfigureAll(Action<ICache> initAction)
        {
            _configurators.Add(new CacheConfigurator(initAction));
        }
        #endregion
       
       
    }
}
