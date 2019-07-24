using AbpFramework.Configuration.Startup;
using AbpFramework.Dependency;
using Castle.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Modules
{
    /// <summary>
    /// 模块基类，所有定义的模块均需继承此抽象类。
    /// </summary>
    /// <remarks>
    /// 模块定义类通常位于其自己的程序集中，
    /// ，还定义了依赖的模块。
    /// </remarks>
    public abstract class AbpModule
    { 
        /// <summary>
        /// IOC容器
        /// </summary>
        protected internal IIocManager IocManager { get; internal set; }
        /// <summary>
        /// 配置文件
        /// </summary>
        protected internal IAbpStartupConfiguration Configuration { get; internal set; }
        protected AbpModule()
        {
            //IocManager = Dependency.IocManager.Instance;
            Logger = NullLogger.Instance;
        }

        /// <summary>
        /// 预加载事件 
        /// 代码可以放在这里，在依赖注入注册之前运行。
        /// </summary>
        public virtual void PreInitialize()
        {
            //IocManager.AddConventionalRegistrar(new BasicConventionalRegistrar());
        }

        /// <summary>
        /// 注册此模块的依赖关系。
        /// </summary>
        public virtual void Initialize()
        {

        }

        /// <summary>
        /// 最后在应用程序启动时被调用。
        /// </summary>
        public virtual void PostInitialize()
        {

        }

        /// <summary>
        /// 当应用程序正在关闭时调用此方法。
        /// </summary>
        public virtual void Shutdown()
        {

        }
        public virtual Assembly[] GetAdditionalAssemblies()
        {
            return new Assembly[0];
        }
        /// <summary>
        /// 是否是ABP module类
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsAbpModule(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            return
                typeInfo.IsClass &&
                !typeInfo.IsAbstract &&
                !typeInfo.IsGenericType &&
                typeof(AbpModule).IsAssignableFrom(type);
        }




        /// <summary>
        /// 查找模块的直接依赖模块（不包括给定的模块）。
        /// </summary>
        public static List<Type> FindDependedModuleTypes(Type moduleType)
        {
            if (!IsAbpModule(moduleType))
            {
                throw new Exception("This type is not an ABP module: " + moduleType.AssemblyQualifiedName);
            }

            var list = new List<Type>();

            if (moduleType.GetTypeInfo().IsDefined(typeof(DependsOnAttribute), true))
            {
                var dependsOnAttributes = moduleType.GetTypeInfo().GetCustomAttributes(typeof(DependsOnAttribute), true).Cast<DependsOnAttribute>();
                foreach (var dependsOnAttribute in dependsOnAttributes)
                {
                    foreach (var dependedModuleType in dependsOnAttribute.DependedModuleTypes)
                    {
                        list.Add(dependedModuleType);
                    }
                }
            }

            return list;
        }

        public static List<Type> FindDependedModuleTypesRecursivelyIncludingGivenModule(Type moduleType)
        {
            var list = new List<Type>();
            AddModuleAndDependenciesRecursively(list, moduleType);
            list.AddIfNotContains(typeof(AbpKernelModule));
            
            return list;
        }

        private static void AddModuleAndDependenciesRecursively(List<Type> modules, Type module)
        {
            if (!IsAbpModule(module))
            {
                throw new Exception("This type is not an ABP module: " + module.AssemblyQualifiedName);
            }

            if (modules.Contains(module))
            {
                return;
            }

            modules.Add(module);

            var dependedModules = FindDependedModuleTypes(module);
            foreach (var dependedModule in dependedModules)
            {
                AddModuleAndDependenciesRecursively(modules, dependedModule);
            }
        }
        /// <summary>
        /// 日志.
        /// </summary>
        public ILogger Logger { get; set; }


    }
}
