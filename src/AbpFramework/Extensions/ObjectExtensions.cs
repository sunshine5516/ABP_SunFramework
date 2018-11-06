using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
namespace AbpFramework.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// 将给定对象转换为值类型
        /// </summary>
        /// <param name="obj">待转换类型</param>
        /// <typeparam name="T">目标类型</typeparam>
        /// <returns>Converted object</returns>
        public static T To<T>(this object obj)
            where T:struct
        {
            if (typeof(T) == typeof(Guid))
            {
                return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(obj.ToString());
            }

            return (T)Convert.ChangeType(obj, typeof(T), CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// 简化将对象转换为类型的过程。
        /// </summary>
        /// <typeparam name="T">带转换类型</typeparam>
        /// <param name="obj">目标类型</param>
        /// <returns>Casted object</returns>
        public static T As<T>(this object obj)
             where T : class
        {
            return (T)obj;
        }
        /// <summary>
        /// 集合是否包含该元素.
        /// </summary>
        /// <param name="item">Item to check</param>
        /// <param name="list">List of items</param>
        /// <typeparam name="T">Type of the items</typeparam>
        public static bool IsIn<T>(this T item, params T[] list)
        {
            return list.Contains(item);
        }
    }
}
