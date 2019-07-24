using AbpFramework.Domain.Entities;
using AbpFramework.Domain.Repositories;
using System.Data.Entity;
namespace Abp.EntityFramework.EntityFramework.Repositories
{
    public class EfRepositoryBase<TDbContext, TEntity> : EfRepositoryBase<TDbContext, TEntity, int>, IRepository<TEntity>
        where TEntity : class, IEntity<int>
        where TDbContext : DbContext
    {
        public EfRepositoryBase(IDbContextProvider<TDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }
    }
}
