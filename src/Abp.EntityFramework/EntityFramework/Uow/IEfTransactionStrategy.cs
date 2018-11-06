using AbpFramework.Dependency;
using AbpFramework.Domain.Uow;
using System.Data.Entity;

namespace Abp.EntityFramework.EntityFramework.Uow
{
    public interface IEfTransactionStrategy
    {
        void InitOptions(UnitOfWorkOptions options);
        DbContext CreateDbContext<TDbContext>(string connectionString, IDbContextResolver dbContextResolver)
           where TDbContext : DbContext;
        void Commit();
        void Dispose(IIocResolver iocResolver);
    }
}
