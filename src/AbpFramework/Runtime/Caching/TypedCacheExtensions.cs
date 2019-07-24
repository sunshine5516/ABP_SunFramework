using System;
using System.Threading.Tasks;

namespace AbpFramework.Runtime.Caching
{
    public static class TypedCacheExtensions
    {
        public static Task<TValue> GetAsync<TKey, TValue>(this ITypedCache<TKey, TValue> cache, TKey key, Func<Task<TValue>> factory)
        {
            return cache.GetAsync(key, k => factory());
        }
    }
}
