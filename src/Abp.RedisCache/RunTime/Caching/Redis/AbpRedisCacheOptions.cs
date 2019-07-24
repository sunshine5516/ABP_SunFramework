using AbpFramework.Configuration.Startup;
using AbpFramework.Extensions;
using System.Configuration;
namespace Abp.RedisCache.RunTime.Caching.Redis
{
    public class AbpRedisCacheOptions
    {
        #region 声明实例
        public IAbpStartupConfiguration AbpStartupConfiguration { get; }
        private const string ConnectionStringKey = "Abp.Redis.Cache";

        private const string DatabaseIdSettingKey = "Abp.Redis.Cache.DatabaseId";
        public string ConnectionString { get; set; }
        public int DatabaseId { get; set; }
        #endregion
        #region 构造函数
        public AbpRedisCacheOptions(IAbpStartupConfiguration abpStartupConfiguration)
        {
            AbpStartupConfiguration = abpStartupConfiguration;
            ConnectionString = GetDefaultConnectionString();
            DatabaseId = GetDefaultDatabaseId();
        }
        #endregion
        #region 方法
        private int GetDefaultDatabaseId()
        {
            var appSetting = ConfigurationManager.AppSettings[DatabaseIdSettingKey];
            if (appSetting.IsNullOrEmpty())
            {
                return -1;
            }
            int databaseId;
            if (!int.TryParse(appSetting, out databaseId))
            {
                return -1;
            }
            return databaseId;
        }

        private string GetDefaultConnectionString()
        {
            var connStr = ConfigurationManager.ConnectionStrings[ConnectionStringKey];
            if (connStr == null || connStr.ConnectionString.IsNullOrWhiteSpace())
            {
                return "localhost";
            }

            return connStr.ConnectionString;
        }
        #endregion


    }
}
