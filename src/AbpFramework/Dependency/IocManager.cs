using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
namespace AbpFramework.Dependency
{
    /// <summary>
    /// 直接执行依赖注入任务
    /// </summary>
    public class IocManager : IIocManager
    {
        #region 声明实例
        public static IocManager Instance { get; private set; }
        public IWindsorContainer IocContainer { get; private set; }
        private readonly List<IConventionalDependencyRegistrar> _conventionalRegistrars;
        static IocManager()
        {
            Instance = new IocManager();
        }
        #endregion
        #region 构造函数
        public IocManager()
        {
            IocContainer = new WindsorContainer();
            _conventionalRegistrars = new List<IConventionalDependencyRegistrar>();
            IocContainer.Register(
                Component.For<IocManager, IIocManager, IIocRegistrar, IIocResolver>().UsingFactoryMethod(() => this)
                );
        }
        #endregion
        #region 方法

        #endregion
        /// <summary>
        /// 添加常规注册的依赖注册器
        /// </summary>
        /// <param name="registrar"></param>
        public void AddConventionalRegistrar(IConventionalDependencyRegistrar registrar)
        {
            _conventionalRegistrars.Add(registrar);
        }

        public void Dispose()
        {
            IocContainer.Dispose();
        }
        /// <summary>
        /// 给定类型是否注册
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool IsRegistered(Type type)
        {
            return IocContainer.Kernel.HasComponent(type);
        }
        /// <summary>
        /// 检查给定类型是否在之前注册
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool IsRegistered<T>()
        {
            return IocContainer.Kernel.HasComponent(typeof(T));
        }

        public void Register<T>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton) where T : class
        {
            IocContainer.Register(ApplyLifestyle(Component.For<T>(), lifeStyle));
        }

        public void Register(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            IocContainer.Register(ApplyLifestyle(Component.For(type), lifeStyle));
        }

        public void Register(Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            IocContainer.Register(ApplyLifestyle(Component.For(type, impl).ImplementedBy(impl), lifeStyle));
        }
        public void Register<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
           where TType : class
           where TImpl : class, TType
        {
            IocContainer.Register(ApplyLifestyle(Component.For<TType, TImpl>().ImplementedBy<TImpl>(), lifeStyle));
        }
        /// <summary>
        /// 注册所有常规注册服务机构给定程序集的类型
        /// </summary>
        /// <param name="assembly"></param>
        public void RegisterAssemblyByConvention(Assembly assembly)
        {
            RegisterAssemblyByConvention(assembly, new ConventionalRegistrationConfig());
        }
        /// <summary>
        /// 注册所有常规注册服务机构给定程序集的类型
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="config"></param>
        public void RegisterAssemblyByConvention(Assembly assembly, ConventionalRegistrationConfig config)
        {
            var context = new ConventionalRegistrationContext(assembly, this, config);
            foreach (var register in _conventionalRegistrars)
            {
                register.RegisterAssembly(context);
            }
            if (config.InstallInstallers)
            {
                IocContainer.Install(FromAssembly.Instance(assembly));
            }
        }

        public void Release(object obj)
        {
            IocContainer.Release(obj);
        }
        /// <summary>
        /// 从IOC容器获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>对象实例</returns>
        public T Resolve<T>()
        {
            return IocContainer.Resolve<T>();
        }
        /// <summary>
        /// 从IOC容器获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>对象实例</returns>
        public T Resolve<T>(Type type)
        {
            return (T)IocContainer.Resolve(type);
        }
        /// <summary>
        /// 从IOC容器获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="argumentsAsAnonymousType"></param>
        /// <returns>对象实例</returns>
        public T Resolve<T>(object argumentsAsAnonymousType)
        {
            return IocContainer.Resolve<T>(argumentsAsAnonymousType);
        }
        /// <summary>
        /// 从IOC容器获取对象
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object Resolve(Type type)
        {
            return IocContainer.Resolve(type);
        }
        /// <summary>
        /// 从IOC容器获取对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="argumentsAsAnonymousType"></param>
        /// <returns></returns>
        public object Resolve(Type type, object argumentsAsAnonymousType)
        {
            return IocContainer.Resolve(type, argumentsAsAnonymousType);
        }

        public T[] ResolveAll<T>()
        {
            return IocContainer.ResolveAll<T>();
        }

        public T[] ResolveAll<T>(object argumentsAsAnonymousType)
        {
            return IocContainer.ResolveAll<T>(argumentsAsAnonymousType);
        }

        public object[] ResolveAll(Type type)
        {
            return IocContainer.ResolveAll(type).Cast<object>().ToArray();
        }

        public object[] ResolveAll(Type type, object argumentsAsAnonymousType)
        {
            return IocContainer.ResolveAll(type, argumentsAsAnonymousType).Cast<object>().ToArray();
        }

        #region 辅助方法
        private static ComponentRegistration<T> ApplyLifestyle<T>(ComponentRegistration<T> registration, DependencyLifeStyle lifeStyle)
            where T : class
        {
            switch (lifeStyle)
            {
                case DependencyLifeStyle.Transient:
                    return registration.LifestyleTransient();
                case DependencyLifeStyle.Singleton:
                    return registration.LifestyleSingleton();
                default:
                    return registration;
            }
        }
        #endregion
    }
}
