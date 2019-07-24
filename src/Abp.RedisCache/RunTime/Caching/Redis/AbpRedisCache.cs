using AbpFramework.Runtime.Caching;
using StackExchange.Redis;
using System;
namespace Abp.RedisCache.RunTime.Caching.Redis
{
    public class AbpRedisCache : CacheBase
    {
        #region 声明实例
        private readonly IDatabase _database;
        private readonly IRedisCacheSerializer _serializer;
        #endregion
        #region 构造函数
        public AbpRedisCache(string name,
             IAbpRedisCacheDatabaseProvider redisCacheDatabaseProvider,
            IRedisCacheSerializer redisCacheSerializer):base(name)
        {
            _database = redisCacheDatabaseProvider.GetDatabase();
            _serializer = redisCacheSerializer;
        }
        #endregion
        #region 方法
        public override void Clear()
        {
            throw new NotImplementedException();
        }

        public override object GetOrDefault(string key)
        {
            var objbyte = _database.StringGet(GetLocalizedKey(key));
            return objbyte.HasValue ? Deserialize(objbyte) : null;
        }

        public override void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public override void Set(string key, object value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null)
        {
            var type = value.GetType();
            var t = GetLocalizedKey(key);
            var k = Serialize(value, type);
            _database.StringSet(
                GetLocalizedKey(key),
                Serialize(value, type),
                absoluteExpireTime ?? slidingExpireTime ?? DefaultAbsoluteExpireTime ?? DefaultSlidingExpireTime
                );
        }
        protected virtual string Serialize(object value, Type type)
        {
            return _serializer.Serialize(value, type);
        }
        protected virtual object Deserialize(RedisValue objbyte)
        {
            return _serializer.Deserialize(objbyte);
        }

        protected virtual string GetLocalizedKey(string key)
        {
            var temp= "n:" + Name + ",c:" + key;
            return "n:" + Name + ",c:" + key;
        }
        #endregion

    }
}
