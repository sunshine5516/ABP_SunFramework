using AbpFramework.Configuration.Startup;
using AbpFramework.Dependency;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Domain.Uow
{
    public class DefaultConnectionStringResolver : IConnectionStringResolver, ITransientDependency
    {
        #region 声明实例
        private readonly IAbpStartupConfiguration _configuration;
        #endregion
        #region 构造函数
        public DefaultConnectionStringResolver(IAbpStartupConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion
        #region 方法
        public string GetNameOrConnectionString(ConnectionStringResolveArgs args)
        {
            Check.NotNull(args, nameof(args));
            var defaultConnectionString = _configuration.DefaultNameOrConnectionString;
            if (!string.IsNullOrWhiteSpace(defaultConnectionString))
            {
                return defaultConnectionString;
            }

            if (ConfigurationManager.ConnectionStrings["Default"] != null)
            {
                return "Default";
            }

            if (ConfigurationManager.ConnectionStrings.Count == 1)
            {
                return ConfigurationManager.ConnectionStrings[0].ConnectionString;
            }

            throw new Exception("Could not find a connection string definition for the application. Set IAbpStartupConfiguration.DefaultNameOrConnectionString or add a 'Default' connection string to application .config file.");
        }
        #endregion


    }
}
