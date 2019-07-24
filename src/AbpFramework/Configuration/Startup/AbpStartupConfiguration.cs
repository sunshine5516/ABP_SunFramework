using System;
using System.Collections.Generic;
using AbpFramework.Auditing;
using AbpFramework.BackgroundJobs;
using AbpFramework.Dependency;
using AbpFramework.Domain.Uow;
using AbpFramework.Notifications;
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
        public IBackgroundJobConfiguration BackgroundJobs { get; private set; }
        /// <summary>
        /// 事件总成配置
        /// </summary>
        public IEventBusConfiguration EventBus { get; private set; }
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
            Authorization = IocManager.Resolve<IAuthorizationConfiguration>();
            Validation = IocManager.Resolve<IValidationConfiguration>();
            Caching = IocManager.Resolve<ICachingConfiguration>();
            Modules = IocManager.Resolve<IModuleConfigurations>();
            ServiceReplaceActions = new Dictionary<Type, Action>();
            UnitOfWork = IocManager.Resolve<IUnitOfWorkDefaultOptions>();
            Settings = IocManager.Resolve<ISettingsConfiguration>();
            Navigation = IocManager.Resolve<INavigationConfiguration>();
            MultiTenancy= IocManager.Resolve<IMultiTenancyConfig>();
            BackgroundJobs = IocManager.Resolve<IBackgroundJobConfiguration>();
            EventBus = IocManager.Resolve<IEventBusConfiguration>();
            Notifications = IocManager.Resolve<INotificationConfiguration>();
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
        /// <summary>
        /// 设置配置项
        /// </summary>
        public ISettingsConfiguration Settings { get; private set; }
        /// <summary>
        /// 导航配置项.
        /// </summary>
        public INavigationConfiguration Navigation { get; private set; }
        /// <summary>
        /// Used to configure multi-tenancy.
        /// </summary>
        public IMultiTenancyConfig MultiTenancy { get; private set; }
        /// <summary>
        /// 配置权限
        /// </summary>
        public IAuthorizationConfiguration Authorization { get; private set; }
        /// <summary>
        /// 系统通知配置.
        /// </summary>
        public INotificationConfiguration Notifications { get; private set; }
        #endregion


    }
}
