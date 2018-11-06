using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Reflection
{
    /// <summary>
    /// 定义用于反射的辅助方法。
    /// </summary>
    public static class ReflectionHelper
    {
        //public static TAttribute GetSingleAttributeOfMemberOrDeclaringTypeOrDefault<TAttribute>(MemberInfo memberInfo, TAttribute defaultValue = default(TAttribute), bool inherit = true)
        //    where TAttribute : class
        //{
        //    return memberInfo.GetCustomAttributes(true).OfType<TAttribute>().FirstOrDefault()
        //          ?? memberInfo.DeclaringType?.GetTypeInfo().GetCustomAttributes(true).OfType<TAttribute>().FirstOrDefault()
        //          ?? defaultValue;
        //}
        /// <summary>
        /// 尝试获取为类成员定义的属性，并声明包含继承属性的类型。
        /// 如果没有声明，则返回默认值。
        /// </summary>
        /// <typeparam name="TAttribute">属性的类型</typeparam>
        /// <param name="memberInfo">MemberInfo</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="inherit">Inherit attribute from base classes</param>
        public static TAttribute GetSingleAttributeOrDefault<TAttribute>(MemberInfo memberInfo, TAttribute defaultValue = default(TAttribute), bool inherit = true)
            where TAttribute : Attribute
        {
            if (memberInfo.IsDefined(typeof(TAttribute), inherit))
            {
                return memberInfo.GetCustomAttributes(typeof(TAttribute), inherit).Cast<TAttribute>().First();
            }

            return defaultValue;
        }
        /// <summary>
        /// 尝试获取为类成员定义的属性，它的声明类型包括继承的属性。
        /// 如果没有声明，则返回默认值.
        /// </summary>
        /// <typeparam name="TAttribute">属性的类型</typeparam>
        /// <param name="memberInfo">MemberInfo</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="inherit">从基类继承属性</param>
        public static TAttribute GetSingleAttributeOfMemberOrDeclaringTypeOrDefault<TAttribute>(MemberInfo memberInfo, TAttribute defaultValue = default(TAttribute), bool inherit = true)
            where TAttribute : class
        {
            return memberInfo.GetCustomAttributes(true).OfType<TAttribute>().FirstOrDefault()
                   ?? memberInfo.DeclaringType?.GetTypeInfo().GetCustomAttributes(true).OfType<TAttribute>().FirstOrDefault()
                   ?? defaultValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="givenType"></param>
        /// <param name="genericType"></param>
        /// <returns></returns>
        public static bool IsAssignableToGenericType(Type givenType, Type genericType)
        {
            var givenTypeInfo = givenType.GetTypeInfo();
            if (givenTypeInfo.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
            {
                return true;
            }
            foreach (var interfaceType in givenType.GetInterfaces())
            {
                if (interfaceType.GetTypeInfo().IsGenericType && interfaceType.GetGenericTypeDefinition() == genericType)
                {
                    return true;
                }
            }

            if (givenTypeInfo.BaseType == null)
            {
                return false;
            }

            return IsAssignableToGenericType(givenTypeInfo.BaseType, genericType);
        }
    }
}
