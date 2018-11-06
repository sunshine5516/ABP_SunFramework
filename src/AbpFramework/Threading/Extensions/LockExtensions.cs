using System;
namespace AbpFramework.Threading.Extensions
{
    public static class LockExtensions
    {
        /// <summary>
        /// 通过锁定给定的<paramref name ="source"/>对象来执行给定的<paramref name ="action"/>。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
        public static void Locking<T>(this T source, Action<T> action) where T : class
        {
            lock (source)
            {
                action(source);
            }
        }
    }
}
