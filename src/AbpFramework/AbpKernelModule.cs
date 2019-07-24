using AbpFramework.Application.Navigation;
using AbpFramework.Application.Services;
using AbpFramework.Auditing;
using AbpFramework.Authorization;
using AbpFramework.BackgroundJobs;
using AbpFramework.Configuration;
using AbpFramework.Configuration.Startup;
using AbpFramework.Dependency;
using AbpFramework.Domain.Uow;
using AbpFramework.Events.Bus;
using AbpFramework.Modules;
using AbpFramework.Net.Mail;
using AbpFramework.Notifications;
using AbpFramework.Reflection;
using AbpFramework.Runtime;
using AbpFramework.Runtime.Remoting;
using AbpFramework.Threading.BackgroundWorkers;
namespace AbpFramework
{
    /// <summary>
    /// ABP系统的内核（核心）模块。
    /// </summary>
    public sealed class AbpKernelModule: AbpModule
    {
        public override void PreInitialize()
        {
            IocManager.AddConventionalRegistrar(new BasicConventionalRegistrar());
            IocManager.Register<IScopedIocResolver, ScopedIocResolver>(DependencyLifeStyle.Transient);
            IocManager.Register(typeof(IAmbientScopeProvider<>), typeof(DataContextAmbientScopeProvider<>), DependencyLifeStyle.Transient);
            AddSettingProviders();
            AddAuditingSelectors();
            AddUnitOfWorkFilters();
        }
        public override void Initialize()
        {
            foreach (var replaceAction in ((AbpStartupConfiguration)Configuration).ServiceReplaceActions.Values)
            {
                replaceAction();
            }
            IocManager.IocContainer.Install(new EventBusInstaller(IocManager));
            IocManager.RegisterAssemblyByConvention(typeof(AbpKernelModule).GetAssembly(),
                new ConventionalRegistrationConfig
                {
                    InstallInstallers = false
                });
        }
        public override void PostInitialize()
        {
            RegisterMissingComponents();
            IocManager.Resolve<PermissionManager>().Initialize();
            IocManager.Resolve<SettingDefinitionManager>().Initialize();
            IocManager.Resolve<NavigationManager>().Initialize();
            IocManager.Resolve<NotificationDefinitionManager>().Initialize();
            if(Configuration.BackgroundJobs.IsJobExecutionEnabled)
            {
                var workerManager = IocManager.Resolve<IBackgroundWorkerManager>();
                workerManager.Start();
                workerManager.Add(IocManager.Resolve<IBackgroundJobManager>());
            }
        }
        private void RegisterMissingComponents()
        {
            IocManager.RegisterIfNot<IUnitOfWorkFilterExecuter, NullUnitOfWorkFilterExecuter>(DependencyLifeStyle.Singleton);
            IocManager.RegisterIfNot<IAuditingStore, SimpleLogAuditingStore>(DependencyLifeStyle.Singleton);
            IocManager.RegisterIfNot<IClientInfoProvider, NullClientInfoProvider>(DependencyLifeStyle.Singleton);



            //IocManager.RegisterIfNot<IRealTimeNotifier, SignalRRealTimeNotifier>(DependencyLifeStyle.Singleton);

            if (Configuration.BackgroundJobs.IsJobExecutionEnabled)
            {
                IocManager.RegisterIfNot<IBackgroundJobStore, InMemoryBackgroundJobStore>(DependencyLifeStyle.Singleton);
            }
            else
            {
                IocManager.RegisterIfNot<IBackgroundJobStore, NullBackgroundJobStore>(DependencyLifeStyle.Singleton);
            }
        }
        /// <summary>
        /// 添加审计日志拦截类型
        /// </summary>
        private void AddAuditingSelectors()
        {
            Configuration.Auditing.Selectors.Add(
                new NamedTypeSelector(
                    "Abp.ApplicationServices",
                    type => typeof(IApplicationService).IsAssignableFrom(type)
                    )
                );
        }
        private void AddUnitOfWorkFilters()
        {
            Configuration.UnitOfWork.RegisterFilter(AbpDataFilters.SoftDelete, true);
            Configuration.UnitOfWork.RegisterFilter(AbpDataFilters.MayHaveTenant, true);
            Configuration.UnitOfWork.RegisterFilter(AbpDataFilters.MustHaveTenant, true);
        }
        private void AddSettingProviders()
        {
            Configuration.Settings.Providers.Add<NotificationSettingProvider>();
            Configuration.Settings.Providers.Add<EmailSettingProvider>();
        }
    }
}
