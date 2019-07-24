using StackExchange.Redis;
namespace Abp.RedisCache.RunTime.Caching.Redis
{
    /// <summary>
    /// 获取redis缓存
    /// </summary>
    public interface IAbpRedisCacheDatabaseProvider
    {
        IDatabase GetDatabase();
    }
}
