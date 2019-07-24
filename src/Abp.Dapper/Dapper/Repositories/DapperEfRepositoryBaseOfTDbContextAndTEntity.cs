using AbpFramework.Data;
using AbpFramework.Domain.Entities;
namespace Abp.Dapper.Dapper.Repositories
{
    public class DapperEfRepositoryBase<TDbContext, TEntity>:
        DapperEfRepositoryBase<TDbContext,TEntity,int>,IDapperRepository<TEntity>
        where TEntity:class, IEntity<int>
        where TDbContext : class
    {
        public DapperEfRepositoryBase(IActiveTransactionProvider activeTransactionProvider) 
            : base(activeTransactionProvider)
        { }
    }
}
