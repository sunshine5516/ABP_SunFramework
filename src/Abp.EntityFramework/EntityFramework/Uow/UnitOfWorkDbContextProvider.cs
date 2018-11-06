using System;
using System.Data.Entity;
using AbpFramework.Domain.Uow;
using AbpFramework.MultiTenancy;
namespace Abp.EntityFramework.EntityFramework.Uow
{
    /// <summary>
    /// 继承<see cref="IDbContextProvider{TDbContext}"/>从active unit of work 获取上下文对象
    /// </summary>
    public class UnitOfWorkDbContextProvider<TDbContext> : IDbContextProvider<TDbContext>
        where TDbContext : DbContext
    {
        #region 声明实例
        private readonly ICurrentUnitOfWorkProvider _currentUnitOfWorkProvider;
        #endregion
        #region 构造函数
        public UnitOfWorkDbContextProvider(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider)
        {
            _currentUnitOfWorkProvider = currentUnitOfWorkProvider;
        }
        #endregion
        public TDbContext GetDbContext()
        {
            return GetDbContext(null);
        }

        public TDbContext GetDbContext(MultiTenancySides? multiTenancySide)
        {
           return _currentUnitOfWorkProvider.Current.GetDbContext<TDbContext>(multiTenancySide);
        }
    }
}
