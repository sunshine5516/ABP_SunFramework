using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Runtime.Caching
{
    public interface ICache:IDisposable
    {
        /// <summary>
        /// 唯一名称
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 换成默认过期时间.
        /// 默认1H
        /// 可以通过配置改变
        /// </summary>
        TimeSpan DefaultSlidingExpireTime { get; set; }
        /// <summary>
        /// 缓存项目的默认绝对过期时间。
        /// 默认 null
        /// </summary>
        TimeSpan? DefaultAbsoluteExpireTime { get; set; }
        /// <summary>
        /// 获取缓存实例
        /// 此方法隐藏缓存提供程序失败（并记录它们），如果缓存提供程序失败，则使用工厂方法获取对象。
        /// </summary>
        object Get(string key, Func<string, object> factory);
        /// <summary>
        /// 异步获取缓存实例
        /// </summary>
        /// <param name="key"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        Task<object> GetAsync(string key, Func<string, Task<object>> factory);
        /// <summary>
        /// 获取缓存实例，默认NULL
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns></returns>
        object GetOrDefault(string key);
        /// <summary>
        /// 异步获取缓存实例，默认NULL.
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Cached item or null if not found</returns>
        Task<object> GetOrDefaultAsync(string key);
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <param name="slidingExpireTime">相对时间</param>
        /// <param name="absoluteExpireTime">绝对时间</param>
        void Set(string key,object value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null);
        Task SetAsync(string key, object value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null);
        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);
        /// <summary>
        /// 异步移除缓存
        /// </summary>
        /// <param name="key">Key</param>
        Task RemoveAsync(string key);
        /// <summary>
        /// 清楚缓存
        /// </summary>
        void Clear();

        /// <summary>
        /// 异步清除缓存
        /// </summary>
        Task ClearAsync();
    }
}
