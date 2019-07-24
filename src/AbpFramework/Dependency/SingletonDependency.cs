using System;
namespace AbpFramework.Dependency
{
    public static class SingletonDependency<T>
    {
        public static T Instance => LazyInstance.Value;
        private static readonly Lazy<T> LazyInstance;
        static SingletonDependency()
        {
            LazyInstance = new Lazy<T>(() => IocManager.Instance.Resolve<T>(), true);
        }
    }
}
