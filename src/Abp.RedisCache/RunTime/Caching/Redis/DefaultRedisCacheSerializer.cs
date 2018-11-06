using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbpFramework.Dependency;
using AbpFramework.Json;
using StackExchange.Redis;

namespace Abp.RedisCache.RunTime.Caching.Redis
{
    public class DefaultRedisCacheSerializer : IRedisCacheSerializer, ITransientDependency
    {
        #region 方法
        /// <summary>
        /// 创建一个序列化字符串实例
        /// </summary>
        /// <param name="objbyte">Redis服务器的对象的字符串表示形式.</param>
        /// <returns>新构造对象</returns>
        /// <seealso cref="Serialize" />
        public virtual object Deserialize(RedisValue objbyte)
        {
            return JsonSerializationHelper.DeserializeWithType(objbyte);
        }

        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="value">待序列化对象.</param>
        /// <param name="type">对象类型.</param>
        /// <returns>.</returns>
        /// <seealso cref="Deserialize" />
        public virtual string Serialize(object value, Type type)
        {
            return JsonSerializationHelper.SerializeWithType(value, type);
        }
        #endregion

    }
}
