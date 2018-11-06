using AbpFramework.Auditing;
using AbpFramework.Configuration.Startup;
using AbpFramework.Dependency;
using AbpFramework.Dependency.Installers;
using AbpFramework.Domain.Uow;
using AbpFramework.Modules;
using AbpFramework.Runtime.Validation.Interception;
using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework
{
    /// <summary>
    /// 整个项目启动类
    /// 准备依赖注入并注册启动所需的核心组件。
    /// 必须首先在应用程序中实例化并初始化（请参阅<请参阅cref="Initialize"/>）。
    /// </summary>
    public class AbpBootstrapper : IDisposable
    {
        #region 声明实例
        private AbpModuleManager _moduleManager;
        protected bool IsDisposed;
        /// <summary>
        /// 获取依赖于其他使用模块的应用程序的启动模块
        /// </summary>
        public Type StartupModule { get; }
        /// <summary>
        /// IIocManager实例
        /// </summary>
        public IIocManager IocManager { get;  }
        private ILogger _logger;
        #endregion
        #region 构造函数

        public AbpBootstrapper([NotNull] Type startupModule, [CanBeNull] Action<AbpBootstrapperOptions> optionsAction = null)
        {
            var options = new AbpBootstrapperOptions();
            optionsAction?.Invoke(options);
            StartupModule = startupModule;

            IocManager = options.IocManager;

            _logger = NullLogger.Instance;
            if (!options.DisableAllInterceptors)
            {
                AddInterceptorRegistrars();
            }
        }
        #endregion
        #region 方法
        public static AbpBootstrapper Create<TStartupModule>([CanBeNull] Action<AbpBootstrapperOptions> optionsAction = null)
            where TStartupModule : AbpModule
        {
            return new AbpBootstrapper(typeof(TStartupModule), optionsAction);
        }
        /// <summary>
        /// 注册拦截器
        /// </summary>
        private void AddInterceptorRegistrars()
        {
            ValidationInterceptorRegistrar.Initialize(IocManager);
            UnitOfWorkRegistrar.Initialize(IocManager);
            AuditingInterceptorRegistrar.Initialize(IocManager);//审计拦截器
        }
        public virtual void Initialize()
        {
            ResolveLogger();
            try 
            {
                RegisterBootstrapper();
                //AbpCoreInstaller注册的是系统框架级（核心框架，也就是指Abp项目）的所有configuration
                IocManager.IocContainer.Install(new AbpCoreInstaller());

                IocManager.Resolve<AbpStartupConfiguration>().Initialize();
                _moduleManager = IocManager.Resolve<AbpModuleManager>();
                _moduleManager.Initialize(StartupModule);
                _moduleManager.StartModules();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private void ResolveLogger()
        {
            if (IocManager.IsRegistered<ILoggerFactory>())
            {
                _logger = IocManager.Resolve<ILoggerFactory>().Create(typeof(AbpBootstrapper));
            }
        }
        public void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            IsDisposed = true;
        }
        public void RegisterBootstrapper()
        {
            if (!IocManager.IsRegistered<AbpBootstrapper>())
            {
                IocManager.IocContainer.Register(
                    Component.For<AbpBootstrapper>().Instance(this)
                    );
            }
        }
        #endregion

    }
}
