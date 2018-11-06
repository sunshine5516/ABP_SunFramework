using AbpFramework.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Abp.WebApi.Controllers.Dynamic.Builders
{
    internal static class DynamicApiControllerActionHelper
    {
        /// <summary>
        /// 获取方法类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<MethodInfo> GetMethodsOfType(Type type)
        {
            var allMethods = new List<MethodInfo>();
            FillMethodsRecursively(type, BindingFlags.Public | BindingFlags.Instance, allMethods);
            return allMethods.Where(method => method.DeclaringType != typeof(object) &&
                          method.DeclaringType != typeof(ApplicationService) &&
                          !IsPropertyAccessor(method)
                ).ToList();
        }
        /// <summary>
        /// 递归填充方法
        /// </summary>
        /// <param name="type"></param>
        /// <param name="flags"></param>
        /// <param name="members"></param>
        private static void FillMethodsRecursively(Type type, BindingFlags flags, List<MethodInfo> members)
        {
            members.AddRange(type.GetMethods(flags));
            foreach (var interfaceType in type.GetInterfaces())
            {
                FillMethodsRecursively(interfaceType, flags, members);
            }
        }
        private static bool IsPropertyAccessor(MethodInfo method)
        {
            return method.IsSpecialName && (method.Attributes & MethodAttributes.HideBySig) != 0;
        }

        public static bool IsMethodOfType(MethodInfo methodInfo, Type type)
        {
            if (type.IsAssignableFrom(methodInfo.DeclaringType))
            {
                return true;
            }

            if (!type.IsInterface)
            {
                return false;
            }

            return type.GetInterfaces().Any(interfaceType => IsMethodOfType(methodInfo, interfaceType));
        }
    }
}
