using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Dependency
{
    /// <summary>
    /// 该接口用于直接执行依赖注入任务
    /// </summary>
    public interface IIocManager : IIocRegistrar, IIocResolver, IDisposable
    {
        /// <summary>
        /// IWindsorContainer注入容器
        /// </summary>
        IWindsorContainer IocContainer { get; }
        /// <summary>
        /// 类型是否已经注册
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        new bool IsRegistered (Type type);
        /// <summary>
        /// 类型是否已经注册
        /// </summary>
        /// <typeparam name="T">Type to check</typeparam>
        new bool IsRegistered<T>();
    }
}
