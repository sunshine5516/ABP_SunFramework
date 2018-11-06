using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Dependency
{
    /// <summary>
    /// 定义用于依赖解析的类的接口
    /// </summary>
    public interface IIocResolver
    {
        /// <summary>
        /// 从IOC容器获取对象。
        /// Returning object must be Released (see <see cref="Release"/>) after usage.
        /// </summary> 
        /// <typeparam name="T">获取的对象的类型</typeparam>
        /// <returns>对象实例</returns>
        T Resolve<T>();
        /// <summary>
        /// 从IOC容器获取对象.
        /// Returning object must be Released (see <see cref="Release"/>) after usage.
        /// </summary> 
        /// <typeparam name="T">要投射的物体的类型</typeparam>
        /// <param name="type">要解析的对象的类型</param>
        /// <returns>对象实例</returns>
        T Resolve<T>(Type type);
        /// <summary>
        /// 从IOC容器获取对象
        /// Returning object must be Released (see <see cref="Release"/>) after usage.
        /// </summary> 
        /// <typeparam name="T">要获取的对象的类型</typeparam>
        /// <param name="argumentsAsAnonymousType">构造函数参数</param>
        /// <returns>对象实例</returns>
        T Resolve<T>(object argumentsAsAnonymousType);
        /// <summary>
        /// 从IOC容器获取对象
        /// </summary>
        /// <param name="type">要获取的对象的类型</param>
        /// <returns>对象实例</returns>
        object Resolve(Type type);
        /// <summary>
        /// 从IOC容器获取对象.
        /// Returning object must be Released (see <see cref="Release"/>) after usage.
        /// </summary> 
        /// <param name="type">要获取的对象的类型</param>
        /// <param name="argumentsAsAnonymousType">构造函数参数s</param>
        /// <returns>对象实例</returns>
        object Resolve(Type type, object argumentsAsAnonymousType);
        /// <summary>
        /// 获取给定类型的所有实现
        /// </summary>
        /// <typeparam name="T">要解析的对象的类型</typeparam>
        /// <returns>对象实例</returns>
        T[] ResolveAll<T>();
        /// <summary>
        /// 获取给定类型的所有实现.
        /// Returning objects must be Released (see <see cref="Release"/>) after usage.
        /// </summary> 
        /// <typeparam name="T">要解析的对象的类型</typeparam>
        /// <param name="argumentsAsAnonymousType">构造函数参数</param>
        /// <returns>对象实例</returns>
        T[] ResolveAll<T>(object argumentsAsAnonymousType);
        /// <summary>
        /// 获取给定类型的所有实现
        /// Returning objects must be Released (see <see cref="Release"/>) after usage.
        /// </summary> 
        /// <param name="type">要解析的对象的类型</param>
        /// <returns>对象实例</returns>
        object[] ResolveAll(Type type);
        /// <summary>
        /// 获取给定类型的所有实现.
        /// Returning objects must be Released (see <see cref="Release"/>) after usage.
        /// </summary> 
        /// <param name="type">要解析的对象的类型</param>
        /// <param name="argumentsAsAnonymousType">构造函数名</param>
        /// <returns>对象实例</returns>
        object[] ResolveAll(Type type, object argumentsAsAnonymousType);
        /// <summary>
        /// 释放预先解析的对象。 请参阅解析方法。
        /// </summary>
        /// <param name="obj">要释放的对象d</param>
        void Release(object obj);
        /// <summary>
        /// 检查给定类型是否在之前注册.
        /// </summary>
        /// <param name="type">检测类型</param>
        bool IsRegistered(Type type);
        /// <summary>
        /// Checks whether given type is registered before.
        /// </summary>
        /// <typeparam name="T">Type to check</typeparam>
        bool IsRegistered<T>();
    }
}
