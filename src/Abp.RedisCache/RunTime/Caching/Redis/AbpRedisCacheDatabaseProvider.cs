using AbpFramework.Dependency;
using StackExchange.Redis;
using System;
namespace Abp.RedisCache.RunTime.Caching.Redis
{
    public class AbpRedisCacheDatabaseProvider : IAbpRedisCacheDatabaseProvider, ISingletonDependency
    {
        #region 声明实例
        private readonly AbpRedisCacheOptions _options;
        private readonly Lazy<ConnectionMultiplexer> _connectionMultiplexer;
        #endregion
        #region 构造函数

        #endregion
        #region 方法
        public AbpRedisCacheDatabaseProvider(AbpRedisCacheOptions options)
        {
            _options = options;
            _connectionMultiplexer = new Lazy<ConnectionMultiplexer>(CreateConnectionMultiplexer);
        }

        private ConnectionMultiplexer CreateConnectionMultiplexer()
        {
            return ConnectionMultiplexer.Connect(_options.ConnectionString);
        }

        /// <summary>
        /// 数据库连接
        /// </summary>
        public IDatabase GetDatabase()
        {
            return _connectionMultiplexer.Value.GetDatabase(_options.DatabaseId);
        }
        #endregion

    }
}
