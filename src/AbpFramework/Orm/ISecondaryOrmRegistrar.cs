using AbpFramework.Dependency;
using AbpFramework.Domain.Repositories;
namespace AbpFramework.Orm
{
    public interface ISecondaryOrmRegistrar
    {
        string OrmContextKey { get; }
        void RegisterRepositories(IIocManager iocManager, AutoRepositoryTypesAttribute defaultRepositoryTypes);
    }
}
