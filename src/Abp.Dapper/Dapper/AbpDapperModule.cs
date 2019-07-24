using AbpFramework;
using AbpFramework.Dependency;
using AbpFramework.Modules;
using AbpFramework.Orm;
using AbpFramework.Reflection.Extensions;
using System.Reflection;

namespace Abp.Dapper.Dapper
{
    [DependsOn(typeof(AbpKernelModule))]
    public class AbpDapperModule: AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactionScopeAvailable = false;
        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpDapperModule).GetAssembly());
            //IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            using (IScopedIocResolver scope = IocManager.CreateScope())
            {
                ISecondaryOrmRegistrar[] additionalOrmRegistrars= scope.ResolveAll<ISecondaryOrmRegistrar>();
                foreach(ISecondaryOrmRegistrar registrar in additionalOrmRegistrars)
                {
                    if(registrar.OrmContextKey==AbpConsts.Orms.EntityFramework)
                    {
                        registrar.RegisterRepositories(IocManager, EfBasedDapperAutoRepositoryTypes.Default);
                    }
                    if(registrar.OrmContextKey==AbpConsts.Orms.NHibernate)
                    {
                        registrar.RegisterRepositories(IocManager, NhBasedDapperAutoRepositoryTypes.Default);
                    }
                    if(registrar.OrmContextKey==AbpConsts.Orms.EntityFrameworkCore)
                    {
                        registrar.RegisterRepositories(IocManager, EfBasedDapperAutoRepositoryTypes.Default);
                    }
                }
            }
        }
    }
}
