using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.RedisCache.RunTime.Caching.Redis
{
    /// <summary>
    /// 从Redis缓存中持久保存和检索对象时使用的所有自定义（de）序列化方法实现的接口。
    /// </summary>
    public interface IRedisCacheSerializer
    {
        /// <summary>
        /// 创建一个序列化字符串实例
        /// </summary>
        /// <param name="objbyte">Redis服务器的对象的字符串表示形式.</param>
        /// <returns>新构造对象</returns>
        /// <seealso cref="Serialize" />
        object Deserialize(RedisValue objbyte);
        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="value">待序列化对象.</param>
        /// <param name="type">对象类型.</param>
        /// <returns>.</returns>
        /// <seealso cref="Deserialize" />
        string Serialize(object value, Type type);
    }
}
