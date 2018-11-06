using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Dependency
{
    public static class IocResolverExtensions
    {
        public static IDisposableDependencyObjectWrapper<T> ResolveAsDisposable<T>(this IIocResolver iocResolver)
        {
            return new DisposableDependencyObjectWrapper<T>(iocResolver, iocResolver.Resolve<T>());
        }

        /// <summary>
        /// Gets an <see cref="DisposableDependencyObjectWrapper{T}"/> object that wraps resolved object to be Disposable.
        /// </summary> 
        /// <typeparam name="T">Type of the object to get</typeparam>
        /// <param name="iocResolver">IIocResolver object</param>
        /// <param name="type">Type of the object to resolve. This type must be convertible <typeparamref name="T"/>.</param>
        /// <returns>The instance object wrapped by <see cref="DisposableDependencyObjectWrapper{T}"/></returns>
        public static IDisposableDependencyObjectWrapper<T> ResolveAsDisposable<T>(this IIocResolver iocResolver, Type type)
        {
            return new DisposableDependencyObjectWrapper<T>(iocResolver, (T)iocResolver.Resolve(type));
        }

    }
}
