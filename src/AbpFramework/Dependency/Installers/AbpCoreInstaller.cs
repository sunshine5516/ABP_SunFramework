using AbpFramework.Auditing;
using AbpFramework.BackgroundJobs;
using AbpFramework.Configuration.Startup;
using AbpFramework.Domain.Uow;
using AbpFramework.Modules;
using AbpFramework.Notifications;
using AbpFramework.Reflection;
using AbpFramework.Runtime.Caching.Configuration;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
namespace AbpFramework.Dependency.Installers
{
    internal class AbpCoreInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IMultiTenancyConfig, MultiTenancyConfig>().ImplementedBy<MultiTenancyConfig>().LifestyleSingleton(),
                Component.For<INavigationConfiguration, NavigationConfiguration>().ImplementedBy<NavigationConfiguration>().LifestyleSingleton(),
                Component.For<IUnitOfWorkDefaultOptions, UnitOfWorkDefaultOptions>().ImplementedBy<UnitOfWorkDefaultOptions>().LifestyleSingleton(),
                Component.For<IValidationConfiguration, ValidationConfiguration>().ImplementedBy<ValidationConfiguration>().LifestyleSingleton(),
                Component.For<IAbpModuleManager, AbpModuleManager>().ImplementedBy<AbpModuleManager>().LifestyleSingleton(),
                Component.For<IAbpStartupConfiguration, AbpStartupConfiguration>().ImplementedBy<AbpStartupConfiguration>().LifestyleSingleton(),
                Component.For<ICachingConfiguration, CachingConfiguration>().ImplementedBy<CachingConfiguration>().LifestyleSingleton(),
                Component.For<IModuleConfigurations, ModuleConfigurations>().ImplementedBy<ModuleConfigurations>().LifestyleSingleton(),
                Component.For<ITypeFinder, TypeFinder>().ImplementedBy<TypeFinder>().LifestyleSingleton(),
                Component.For<ISettingsConfiguration, SettingsConfiguration>().ImplementedBy<SettingsConfiguration>().LifestyleSingleton(),
                Component.For<IAssemblyFinder, AbpAssemblyFinder>().ImplementedBy<AbpAssemblyFinder>().LifestyleSingleton(),
                Component.For<IAuditingConfiguration, AuditingConfiguration>().ImplementedBy<AuditingConfiguration>().LifestyleSingleton(),
                Component.For<IAuthorizationConfiguration, AuthorizationConfiguration>().ImplementedBy<AuthorizationConfiguration>().LifestyleSingleton(),
                Component.For<IBackgroundJobConfiguration, BackgroundJobConfiguration>().ImplementedBy<BackgroundJobConfiguration>().LifestyleSingleton(),
                Component.For<IEventBusConfiguration,EventBusConfiguration>().ImplementedBy<EventBusConfiguration>().LifestyleSingleton(),
                Component.For<INotificationConfiguration, NotificationConfiguration>().ImplementedBy<NotificationConfiguration>().LifestyleSingleton()
                //Component.For<IBackgroundJobConfiguration, BackgroundJobConfiguration>().ImplementedBy<BackgroundJobConfiguration>().LifestyleSingleton()
                );



        }
    }
}
