using AbpFramework.Data;
using AbpFramework.Domain.Entities;
using System.Data.Common;
namespace Abp.Dapper.Dapper.Repositories
{
    public class DapperEfRepositoryBase<TDbContext,TEntity,TPrimaryKey>
        : DapperRepositoryBase<TEntity,TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        #region 声明实例
        private readonly IActiveTransactionProvider _activeTransactionProvider;
        #endregion
        #region 构造函数
        public DapperEfRepositoryBase(IActiveTransactionProvider activeTransactionProvider)
            :base(activeTransactionProvider)
        {
            _activeTransactionProvider = activeTransactionProvider;
        }
        #endregion
        #region 方法
        public ActiveTransactionProviderArgs ActiveTransactionProviderArgs
        {
            get
            {
                return new ActiveTransactionProviderArgs
                {
                    ["ContextType"] = typeof(TDbContext),
                    ["MultiTenancySide"] = MultiTenancySide
                };
            }
        }
        public override DbConnection Connection =>
            (DbConnection)_activeTransactionProvider.GetActiveConnection(ActiveTransactionProviderArgs);
        public override DbTransaction ActiveTransaction =>
            (DbTransaction)_activeTransactionProvider.GetActiveTransaction(ActiveTransactionProviderArgs);
        #endregion
    }
}
