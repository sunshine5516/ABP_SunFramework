using Abp.TestBase.TestBase.Runtime;
using AbpFramework;
using AbpFramework.Dependency;
using AbpFramework.Domain.Uow;
using AbpFramework.Modules;
using AbpFramework.Runtime.Session;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Abp.TestBase.TestBase
{
    /// <summary>
    /// 集成的所有测试的基类
    /// </summary>
    /// <typeparam name="TStartupModule"></typeparam>
    public abstract class AbpIntegratedTestBase<TStartupModule> : IDisposable
        where TStartupModule : AbpModule
    {
        #region 声明实例
        protected IIocManager LocalIocManager { get; }
        protected AbpBootstrapper AbpBootstrapper { get; }
        /// <summary>
        /// 获取Session对象。 可用于在测试中更改当前用户和租户。
        /// </summary>
        protected TestAbpSession AbpSession { get; private set; }
        #endregion
        #region 构造函数
        protected AbpIntegratedTestBase(bool initializeAbp = true)
        {
            LocalIocManager = new IocManager();
            AbpBootstrapper = AbpBootstrapper.Create<TStartupModule>(options =>
              {
                  options.IocManager = LocalIocManager;
              });
            if(initializeAbp)
            {
                InitializeAbp();
            }
        }
        #endregion
        #region 方法
        protected void InitializeAbp()
        {
            LocalIocManager.RegisterIfNot<IAbpSession, TestAbpSession>();
            PreInitialize();
            AbpBootstrapper.Initialize();
            PostInitialize();
            AbpSession = LocalIocManager.Resolve<TestAbpSession>();
        }
        /// <summary>
        /// This method can be overrided to replace some services with fakes.
        /// </summary>
        protected virtual void PreInitialize()
        {

        }

        /// <summary>
        /// This method can be overrided to replace some services with fakes.
        /// </summary>
        protected virtual void PostInitialize()
        {

        }
        public void Dispose()
        {
            AbpBootstrapper.Dispose();
            LocalIocManager.Dispose();
        }
        protected T Resolve<T>()
        {
            EnsureClassRegistered(typeof(T));
            return LocalIocManager.Resolve<T>();
        }
        /// <summary>
        /// 解析对象的快捷方式
        /// </summary>
        /// <typeparam name="T">Type of the object to get</typeparam>
        /// <param name="argumentsAsAnonymousType">Constructor arguments</param>
        /// <returns>The object instance</returns>
        protected T Resolve<T>(object argumentsAsAnonymousType)
        {
            EnsureClassRegistered(typeof(T));
            return LocalIocManager.Resolve<T>(argumentsAsAnonymousType);
        }

        /// <summary>
        /// 解析对象的快捷方式
        /// </summary>
        /// <param name="type">Type of the object to get</param>
        /// <returns>The object instance</returns>
        protected object Resolve(Type type)
        {
            EnsureClassRegistered(type);
            return LocalIocManager.Resolve(type);
        }

        /// <summary>
        /// 解析对象的快捷方式
        /// </summary>
        /// <param name="type">Type of the object to get</param>
        /// <param name="argumentsAsAnonymousType">Constructor arguments</param>
        /// <returns>The object instance</returns>
        protected object Resolve(Type type, object argumentsAsAnonymousType)
        {
            EnsureClassRegistered(type);
            return LocalIocManager.Resolve(type, argumentsAsAnonymousType);
        }
        /// <summary>
        /// 如果之前没有注册，则注册类型。
        /// </summary>
        /// <param name="type">Type to check and register</param>
        /// <param name="lifeStyle">Lifestyle</param>
        protected void EnsureClassRegistered(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Transient)
        {
            if (!LocalIocManager.IsRegistered(type))
            {
                if(!type.GetTypeInfo().IsClass||type.GetTypeInfo().IsAbstract)
                {
                    throw new AbpException("Can not register " + type.Name + ". It should be a non-abstract class. If not, it should be registered before.");
                }
                LocalIocManager.Register(type, lifeStyle);
            }
        }
        protected virtual void WithUnitOfWork(Action action, UnitOfWorkOptions options = null)
        {
            using (var uowManager = LocalIocManager.ResolveAsDisposable<IUnitOfWorkManager>())
            {
                using (var uow = uowManager.Object.Begin(options ?? new UnitOfWorkOptions()))
                {
                    action();
                    uow.Complete();
                }
            }
        }

        protected virtual async Task WithUnitOfWorkAsync(Func<Task> action, UnitOfWorkOptions options = null)
        {
            using (var uowManager = LocalIocManager.ResolveAsDisposable<IUnitOfWorkManager>())
            {
                using (var uow = uowManager.Object.Begin(options ?? new UnitOfWorkOptions()))
                {
                    await action();
                    uow.Complete();
                }
            }
        }
        #endregion

    }
}
