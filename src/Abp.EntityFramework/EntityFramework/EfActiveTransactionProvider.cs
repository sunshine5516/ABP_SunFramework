using AbpFramework.Data;
using AbpFramework.Dependency;
using AbpFramework.MultiTenancy;
using System;
using System.Data;
using System.Data.Entity;
namespace Abp.EntityFramework.EntityFramework
{
    public class EfActiveTransactionProvider : IActiveTransactionProvider, ITransientDependency
    {
        private readonly IIocResolver _iocResolver;
        public EfActiveTransactionProvider(IIocResolver iocResolver)
        {
            _iocResolver = iocResolver;
        }
        public IDbConnection GetActiveConnection(ActiveTransactionProviderArgs args)
        {
            return GetDbContext(args).Database.Connection;
        }

        public IDbTransaction GetActiveTransaction(ActiveTransactionProviderArgs args)
        {
            return GetDbContext(args).Database.CurrentTransaction.UnderlyingTransaction;
        }

        private DbContext GetDbContext(ActiveTransactionProviderArgs args)
        {
            var dbContextProviderType=typeof(IDbContextProvider<>).MakeGenericType((Type)args["ContextType"]);
            using (var dbContextProviderWrapper = _iocResolver.ResolveAsDisposable(dbContextProviderType))
            {
                var method = dbContextProviderWrapper.Object.GetType()
                    .GetMethod(
                    nameof(IDbContextProvider<AbpDbContext>.GetDbContext),
                        new[] { typeof(MultiTenancySides) }
                    );
                return (DbContext)method.Invoke(
                   dbContextProviderWrapper.Object,
                   new object[] { (MultiTenancySides?)args["MultiTenancySide"] }
               );
            }
        }
    }
}
