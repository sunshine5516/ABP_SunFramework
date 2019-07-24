using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Dependency
{
    public class ScopedIocResolver : IScopedIocResolver
    {
        #region 声明实例
        private readonly IIocResolver _iocResolver;
        private readonly List<object> _resolvedObjects;
        #endregion
        #region 构造函数
        public ScopedIocResolver(IIocResolver iocResolver)
        {
            _iocResolver = iocResolver;
            _resolvedObjects = new List<object>();
        }
        #endregion
        #region 方法

        #endregion
        public void Dispose()
        {
            _resolvedObjects.ForEach(_iocResolver.Release);
        }

        public bool IsRegistered(Type type)
        {
            throw new NotImplementedException();
        }

        public bool IsRegistered<T>()
        {
            throw new NotImplementedException();
        }

        public void Release(object obj)
        {
            throw new NotImplementedException();
        }

        public T Resolve<T>()
        {
            return Resolve<T>(typeof(T));
        }

        public T Resolve<T>(Type type)
        {
            return (T)Resolve(type);
        }

        public T Resolve<T>(object argumentsAsAnonymousType)
        {
            throw new NotImplementedException();
        }

        public object Resolve(Type type)
        {
            return Resolve(type, null);
        }

        public object Resolve(Type type, object argumentsAsAnonymousType)
        {
            var resolvedObject = argumentsAsAnonymousType != null ?
                _iocResolver.Resolve(type, argumentsAsAnonymousType)
                : _iocResolver.Resolve(type);
            _resolvedObjects.Add(resolvedObject);
            return resolvedObject;
        }

        public T[] ResolveAll<T>()
        {
            return ResolveAll(typeof(T)).OfType<T>().ToArray();
        }

        public T[] ResolveAll<T>(object argumentsAsAnonymousType)
        {
            return ResolveAll(typeof(T), argumentsAsAnonymousType).OfType<T>().ToArray();
        }

        public object[] ResolveAll(Type type)
        {
            return ResolveAll(type, null);
        }

        public object[] ResolveAll(Type type, object argumentsAsAnonymousType)
        {
            var resolvedObjects = argumentsAsAnonymousType != null
                ? _iocResolver.ResolveAll(type, argumentsAsAnonymousType)
                : _iocResolver.ResolveAll(type);
            _resolvedObjects.AddRange(resolvedObjects);
            return resolvedObjects;
        }
    }
}
