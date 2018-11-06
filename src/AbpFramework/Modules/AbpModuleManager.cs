using AbpFramework.Configuration.Startup;
using AbpFramework.Dependency;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
namespace AbpFramework.Modules
{
    /// <summary>
    /// 模块管理类，主要用来进行模块管理，比如启动关闭模块
    /// </summary>
    public class AbpModuleManager : IAbpModuleManager
    {
        #region 声明实例
        private AbpModuleCollection _modules;
        private readonly IIocManager _iocManager;
        public AbpModuleInfo StartupModule { get; private set; }
        public IReadOnlyList<AbpModuleInfo> Modules => _modules.ToImmutableList();

        #endregion
        #region 构造函数
        public AbpModuleManager(IIocManager iocManager)
        {
            _iocManager = iocManager;
        }

        #endregion
        #region 方法
        public void Initialize(Type startupModule)
        {
            _modules = new AbpModuleCollection(startupModule);
            LoadAllModules();
        }

        private void LoadAllModules()
        {
            List<Type> plugInModuleTypes;
            var moduleTypes = FindAllModuleTypes(out plugInModuleTypes).Distinct().ToList();
            RegisterModules(moduleTypes);
            CreateModules(moduleTypes, plugInModuleTypes);
            _modules.EnsureKernelModuleToBeFirst();
            SetDependencies();
        }

        private void RegisterModules(ICollection<Type> moduleTypes)
        {
            foreach (var moduleType in moduleTypes)
            {
                _iocManager.RegisterIfNot(moduleType);
            }
        }

        private void CreateModules(ICollection<Type> moduleTypes, List<Type> plugInModuleTypes)
        {
            foreach (var moduleType in moduleTypes)
            {
                var moduleObject = _iocManager.Resolve(moduleType) as AbpModule;
                if (moduleObject == null)
                {
                    throw new Exception("This type is not an ABP module: " + moduleType.AssemblyQualifiedName);
                }

                moduleObject.IocManager = _iocManager;
                moduleObject.Configuration = _iocManager.Resolve<IAbpStartupConfiguration>();

                var moduleInfo = new AbpModuleInfo(moduleType, moduleObject, plugInModuleTypes.Contains(moduleType));

                _modules.Add(moduleInfo);

                if (moduleType == _modules.StartupModuleType)
                {
                    StartupModule = moduleInfo;
                }
            }
        }

        /// <summary>
        /// 查找所有Module类型
        /// </summary>
        /// <param name="plugInModuleTypes"></param>
        /// <returns></returns>
        private List<Type> FindAllModuleTypes(out List<Type> plugInModuleTypes)
        {
            plugInModuleTypes = new List<Type>();
            var modules=AbpModule.FindDependedModuleTypesRecursivelyIncludingGivenModule(_modules.StartupModuleType);
            return modules;
        }
        public void ShutdownModules()
        {
            var sortedModules = _modules.GetSortedModuleListByDependency();
            sortedModules.Reverse();
            sortedModules.ForEach(sm => sm.Instance.Shutdown());
        }

        public void StartModules()
        {
            var sortedModules = _modules.GetSortedModuleListByDependency();
            sortedModules.ForEach(module => module.Instance.PreInitialize());
            sortedModules.ForEach(module => module.Instance.Initialize());
            sortedModules.ForEach(module => module.Instance.PostInitialize());
        }


        private void SetDependencies()
        {
            foreach (var moduleInfo in _modules)
            {
                moduleInfo.Dependencies.Clear();

                //Set dependencies for defined DependsOnAttribute attribute(s).
                foreach (var dependedModuleType in AbpModule.FindDependedModuleTypes(moduleInfo.Type))
                {
                    var dependedModuleInfo = _modules.FirstOrDefault(m => m.Type == dependedModuleType);
                    if (dependedModuleInfo == null)
                    {
                        throw new Exception("Could not find a depended module " + dependedModuleType.AssemblyQualifiedName + " for " + moduleInfo.Type.AssemblyQualifiedName);
                    }

                    if ((moduleInfo.Dependencies.FirstOrDefault(dm => dm.Type == dependedModuleType) == null))
                    {
                        moduleInfo.Dependencies.Add(dependedModuleInfo);
                    }
                }
            }
        }
        #endregion


    }
}
