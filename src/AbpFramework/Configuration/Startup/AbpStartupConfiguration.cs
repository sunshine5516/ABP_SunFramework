using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbpFramework.Auditing;
using AbpFramework.Dependency;
using AbpFramework.Domain.Uow;
using AbpFramework.Runtime.Caching.Configuration;

namespace AbpFramework.Configuration.Startup
{
    public class AbpStartupConfiguration : DictionaryBasedConfig, IAbpStartupConfiguration
    {
        #region 声明实例

        public IValidationConfiguration Validation { get; private set; }
        public IIocManager IocManager { get; }
        public ICachingConfiguration Caching { get; private set; }

        public IModuleConfigurations Modules { get; private set; }
        public string DefaultNameOrConnectionString { get; set; }
        public Dictionary<Type, Action> ServiceReplaceActions { get; private set; }

        #endregion
        #region 构造函数
        public AbpStartupConfiguration(IIocManager IocManager)
        {
            this.IocManager = IocManager;
            Auditing = IocManager.Resolve<IAuditingConfiguration>();
        }
        #endregion
        #region 方法
        public void Initialize()
        {
            Validation = IocManager.Resolve<IValidationConfiguration>();
            Caching = IocManager.Resolve<ICachingConfiguration>();
            Modules = IocManager.Resolve<IModuleConfigurations>();
            ServiceReplaceActions = new Dictionary<Type, Action>();
            UnitOfWork = IocManager.Resolve<IUnitOfWorkDefaultOptions>();
        }
        public T Get<T>()
        {
            return GetOrCreate(typeof(T).FullName, () => IocManager.Resolve<T>());
        }
        public void ReplaceService(Type type, Action replaceAction)
        {
            ServiceReplaceActions[type] = replaceAction;
        }
        /// <summary>
        /// UOW默认配置
        /// </summary>
        public IUnitOfWorkDefaultOptions UnitOfWork { get; private set; }
        /// <summary>
        /// 审计配置.
        /// </summary>
        public IAuditingConfiguration Auditing { get; private set; }
        #endregion


    }
}
