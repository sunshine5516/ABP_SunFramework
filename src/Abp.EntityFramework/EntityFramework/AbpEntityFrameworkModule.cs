using AbpFramework.Modules;
using Abp.EntityFramework.EntityFramework.Interceptors;
using AbpFramework.Reflection;
using AbpFramework.Domain.Uow;
using AbpFramework.Configuration.Startup;
using Castle.MicroKernel.Registration;
using Abp.EntityFramework.EntityFramework.Uow;
using Abp.EntityFramework.Common;
using AbpFramework.Dependency;
using System.Reflection;
using System.Data.Entity.Infrastructure.Interception;
using AbpFramework.Collections.Extensions;
using Abp.EntityFramework.Common.Repositories;
using Abp.EntityFramework.EntityFramework.Repositories;
using System;
using AbpFramework.Orm;
namespace Abp.EntityFramework.EntityFramework
{
    /// <summary>
    /// 该模块用于在EntityFramework中实现“数据访问层”。
    /// </summary>
    [DependsOn(typeof(AbpEntityFrameworkCommonModule))]
    public class AbpEntityFrameworkModule: AbpModule
    {
        #region 声明实例
        public static WithNoLockInterceptor _withNoLockInterceptor;
        private static readonly object WithNoLockInterceptorSyncObj = new object();
        private readonly ITypeFinder _typeFinder;
        #endregion
        #region 构造函数
        public AbpEntityFrameworkModule(ITypeFinder typeFinder)
        {
            this._typeFinder = typeFinder;
        }
        #endregion
        #region 方法
        public override void PreInitialize()
        {
            Configuration.ReplaceService<IUnitOfWorkFilterExecuter>(() =>
            {
                IocManager.IocContainer.Register(
                    Component
                    .For<IUnitOfWorkFilterExecuter, IEfUnitOfWorkFilterExecuter>()
                    .ImplementedBy<EfDynamicFiltersUnitOfWorkFilterExecuter>()
                    .LifestyleTransient()
                );
            });
        }
        public override void Initialize()
        {
            if (!Configuration.UnitOfWork.IsTransactionScopeAvailable)
            {
                IocManager.RegisterIfNot<IEfTransactionStrategy, DbContextEfTransactionStrategy>(DependencyLifeStyle.Transient);
            }
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            IocManager.IocContainer.Register(
                Component.For(typeof(IDbContextProvider<>))
                .ImplementedBy(typeof(UnitOfWorkDbContextProvider<>))
                .LifestyleTransient()
                );
            RegisterGenericRepositoriesAndMatchDbContexes();
            RegisterWithNoLockInterceptor();
        }
        public override void PostInitialize()
        {
           
        }
        #endregion
        #region 辅助方法
        /// <summary>
        /// 注册拦截器
        /// </summary>
        private void RegisterWithNoLockInterceptor()
        {
            lock (WithNoLockInterceptorSyncObj)
            {
                if (_withNoLockInterceptor != null)
                {
                    return;
                }
                _withNoLockInterceptor = IocManager.Resolve<WithNoLockInterceptor>();
                DbInterception.Add(_withNoLockInterceptor);
            }
        }
        /// <summary>
        /// 注册通用存储库并匹配DbContext
        /// </summary>
        private void RegisterGenericRepositoriesAndMatchDbContexes()
        {
            var dbContextTypes =
                _typeFinder.Find(type =>
                type.IsPublic &&
                !type.IsAbstract &&
                type.IsClass &&
                typeof(AbpDbContext).IsAssignableFrom(type));
            if (dbContextTypes.IsNullOrEmpty())
            {
                Logger.Warn("No class found derived from AbpDbContext.");
                return;
            }
            using (var scope = IocManager.CreateScope())
            {
                var repositoryRegistrar = scope.Resolve<IEfGenericRepositoryRegistrar>();
                foreach (var dbContextType in dbContextTypes)
                {
                    Logger.Debug("Registering DbContext: " + dbContextType.AssemblyQualifiedName);
                    repositoryRegistrar.RegisterForDbContext(dbContextType, IocManager, EfAutoRepositoryTypes.Default);

                    IocManager.IocContainer.Register(
                        Component.For<ISecondaryOrmRegistrar>()
                            .Named(Guid.NewGuid().ToString("N"))
                            .Instance(new EfBasedSecondaryOrmRegistrar(dbContextType, scope.Resolve<IDbContextEntityFinder>()))
                            .LifestyleTransient()
                    );
                }
                scope.Resolve<IDbContextTypeMatcher>().Populate(dbContextTypes);
            }
        }
        #endregion
    }
}
