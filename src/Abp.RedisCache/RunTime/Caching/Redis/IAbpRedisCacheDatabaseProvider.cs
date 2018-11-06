using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
