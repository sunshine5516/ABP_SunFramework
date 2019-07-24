using AbpFramework.Dependency;
using AbpFramework.Domain.Entities;
namespace Abp.Dapper.Dapper.Filters.Action
{
    public class DapperActionFilterExecuter : IDapperActionFilterExecuter, ITransientDependency
    {
        #region 声明实例
        private readonly IIocResolver _iocResolver;
        #endregion
        #region 构造函数
        public DapperActionFilterExecuter(IIocResolver iocResolver)
        {
            _iocResolver = iocResolver;
        }
        #endregion
        #region 方法
        public  void ExecuteCreationAuditFilter<TEntity, TPrimaryKey>(TEntity entity)
            where TEntity : class, IEntity<TPrimaryKey>
        {
            _iocResolver.Resolve<CreationAuditDapperActionFilter>().ExecuteFilter<TEntity, TPrimaryKey>(entity);
        }

        public void ExecuteDeletionAuditFilter<TEntity, TPrimaryKey>(TEntity entity)
            where TEntity : class, IEntity<TPrimaryKey>
        {
            _iocResolver.Resolve<DeletionAuditDapperActionFilter>().ExecuteFilter<TEntity, TPrimaryKey>(entity);
        }

        public void ExecuteModificationAuditFilter<TEntity, TPrimaryKey>(TEntity entity)
            where TEntity : class, IEntity<TPrimaryKey>
        {
            _iocResolver.Resolve<ModificationAuditDapperActionFilter>().ExecuteFilter<TEntity, TPrimaryKey>(entity);
        }
        #endregion
    }
}
