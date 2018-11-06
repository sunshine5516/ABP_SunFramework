using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Dependency
{
    /// <summary>
    /// 该接口用于包装从IOC容器解析的对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDisposableDependencyObjectWrapper<out T> : IDisposable
    {        
        /// <summary>
        /// 解析对象
        /// </summary>
        T Object { get; }
    }
}
