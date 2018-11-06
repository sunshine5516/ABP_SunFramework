using AbpFramework.Application.Services;
using AbpFramework.Configuration.Startup;
using AbpFramework.Dependency;
using AbpFramework.Domain.Uow;
using AbpFramework.Modules;
using AbpFramework.Reflection;
using AbpFramework.Runtime;
using AbpFramework.Runtime.Remoting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            IocManager.Register(typeof(IAmbientScopeProvider<>), typeof(DataContextAmbientScopeProvider<>), DependencyLifeStyle.Transient);
            AddAuditingSelectors();
        }
        public override void Initialize()
        {
            foreach (var replaceAction in ((AbpStartupConfiguration)Configuration).ServiceReplaceActions.Values)
            {
                replaceAction();
            }
            IocManager.RegisterAssemblyByConvention(typeof(AbpKernelModule).GetAssembly(),
                new ConventionalRegistrationConfig
                {
                    InstallInstallers = false
                });
        }
        public override void PostInitialize()
        {
            RegisterMissingComponents();
        }
        private void RegisterMissingComponents()
        {
            IocManager.RegisterIfNot<IUnitOfWorkFilterExecuter, NullUnitOfWorkFilterExecuter>(DependencyLifeStyle.Singleton);
        }
        private void AddAuditingSelectors()
        {
            Configuration.Auditing.Selectors.Add(
                new NamedTypeSelector(
                    "Abp.ApplicationServices",
                    type => typeof(IApplicationService).IsAssignableFrom(type)
                    )
                );
        }
    }
}
