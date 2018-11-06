using System;
using System.Collections.Generic;
using System.Data.Entity;
using AbpFramework.Dependency;
using AbpFramework.Domain.Uow;
using AbpFramework.Collections.Extensions;
using System.Transactions;
using AbpFramework.Transactions.Extensions;

namespace Abp.EntityFramework.EntityFramework.Uow
{
    public class DbContextEfTransactionStrategy : IEfTransactionStrategy
    {
        #region 声明实例
        protected UnitOfWorkOptions Options { get; private set; }
        protected IDictionary<string, ActiveTransactionInfo> ActiveTransactions { get; }

        #endregion
        #region 构造函数
        public DbContextEfTransactionStrategy()
        {
            ActiveTransactions = new Dictionary<string, ActiveTransactionInfo>();
        }
        #endregion
        #region 方法
        public void Commit()
        {
            foreach (var activeTransaction in ActiveTransactions.Values)
            {
                activeTransaction.DbContextTransaction.Commit();
            }
        }

        public DbContext CreateDbContext<TDbContext>(string connectionString, IDbContextResolver dbContextResolver) where TDbContext : DbContext
        {
            DbContext dbContext;
            var activeTransaction=ActiveTransactions.GetOrDefault(connectionString);
            if (activeTransaction == null)
            {
                dbContext = dbContextResolver.Resolve<TDbContext>(connectionString);
                var dbtransaction = dbContext.Database.BeginTransaction((Options.IsolationLevel ?? IsolationLevel.ReadUncommitted).ToSystemDataIsolationLevel());
                activeTransaction = new ActiveTransactionInfo(dbtransaction, dbContext);
                ActiveTransactions[connectionString] = activeTransaction;
            }
            else
            {
                dbContext=dbContextResolver.Resolve<TDbContext>(activeTransaction.DbContextTransaction.UnderlyingTransaction.Connection, false);
                dbContext.Database.UseTransaction(activeTransaction.DbContextTransaction.UnderlyingTransaction);
                activeTransaction.AttendedDbContexts.Add(dbContext);
            }
            return dbContext;
        }

        public void Dispose(IIocResolver iocResolver)
        {
            foreach (var activeTransaction in ActiveTransactions.Values)
            {
                foreach (var attendedDbContext in activeTransaction.AttendedDbContexts)
                {
                    iocResolver.Release(attendedDbContext);
                }
                activeTransaction.DbContextTransaction.Dispose();
                iocResolver.Release(activeTransaction.StarterDbContext);
            }
            ActiveTransactions.Clear();
        }

        public void InitOptions(UnitOfWorkOptions options)
        {
            Options = options;
        }
        #endregion

    }
}
