using Abp.Dapper.Dapper.Filters.Action;
using Abp.Dapper.Dapper.Filters.Query;
using AbpFramework.Data;
using AbpFramework.Domain.Entities;
using AbpFramework.Events.Bus.Entities;
using Dapper;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using Abp.Dapper.Dapper.Extensions;
namespace Abp.Dapper.Dapper.Repositories
{
    public class DapperRepositoryBase<TEntity, TPrimaryKey> : AbpDapperRepositoryBase<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        #region 声明实例
        private readonly IActiveTransactionProvider _activeTransactionProvider;
        public IEntityChangeEventHelper EntityChangeEventHelper { get; set; }
        public IDapperQueryFilterExecuter DapperQueryFilterExecuter { get; set; }
        public IDapperActionFilterExecuter DapperActionFilterExecuter { get; set; }

        #endregion
        #region 构造函数
        public DapperRepositoryBase(IActiveTransactionProvider activeTransactionProvider)
        {
            _activeTransactionProvider = activeTransactionProvider;
            EntityChangeEventHelper = NullEntityChangeEventHelper.Instance;
            DapperQueryFilterExecuter = NullDapperQueryFilterExecuter.Instance;
            DapperActionFilterExecuter = NullDapperActionFilterExecuter.Instance;
        }
        #endregion
        #region 方法
        public virtual DbConnection Connection
        {
            get
            {
                return (DbConnection)_activeTransactionProvider.GetActiveConnection
                  (ActiveTransactionProviderArgs.Empty);
            }
        }
        public virtual DbTransaction ActiveTransaction
        {
            get
            {
                return (DbTransaction)_activeTransactionProvider.GetActiveTransaction
                  (ActiveTransactionProviderArgs.Empty);
            }
        }

        public override int Count(Expression<Func<TEntity, bool>> predicate)
        {
            IPredicate filteredPredicate=DapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>(predicate);
            return Connection.Count<TEntity>(filteredPredicate, ActiveTransaction);
        }

        public override void Delete(TEntity entity)
        {
            EntityChangeEventHelper.TriggerEntityDeletingEvent(entity);
            if(entity is ISoftDelete)
            {
                DapperActionFilterExecuter.ExecuteDeletionAuditFilter<TEntity, TPrimaryKey>(entity);
                Connection.Update(entity, ActiveTransaction);
            }
            else
            {
                Connection.Delete(entity, ActiveTransaction);
            }
            EntityChangeEventHelper.TriggerEntityDeletedEventOnUowCompleted(entity);
        }

        public override void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            IEnumerable<TEntity> items = GetAll(predicate);
            foreach(TEntity entity in items)
            {
                Delete(entity);
            }            
        }

        public override int Execute(string query, object parameters = null)
        {
            return Connection.Execute(query, parameters, ActiveTransaction);
        }

        public override TEntity FirstOrDefault(TPrimaryKey id)
        {
            return FirstOrDefault(CreateEqualityExpressionForId(id));
        }

        public override TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            IPredicate pg = DapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>(predicate);
            return Connection.GetList<TEntity>(pg, transaction: ActiveTransaction).FirstOrDefault();
        }

        public override TEntity Get(TPrimaryKey id)
        {
            TEntity item = FirstOrDefault(id);
            if (item == null) { throw new EntityNotFoundException(typeof(TEntity), id); }

            return item;
        }

        public override IEnumerable<TEntity> GetAll()
        {
            PredicateGroup predicateGroup = DapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>();
            return Connection.GetList<TEntity>(predicateGroup, transaction: ActiveTransaction);
        }

        public override IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            IPredicate filteredPredicate = DapperQueryFilterExecuter.ExecuteFilter
                <TEntity, TPrimaryKey>(predicate);
            return Connection.GetList<TEntity>(filteredPredicate, transaction: ActiveTransaction);
        }

        public override IEnumerable<TEntity> GetAllPaged(Expression<Func<TEntity, bool>> predicate,
            int pageNumber, int itemsPerPage,string sortingProperty, bool ascending = true)
        {
            IPredicate filteredPredicate=DapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>(predicate);
            return Connection.GetPage<TEntity>(
                filteredPredicate,
                new List<ISort> { new Sort { Ascending = ascending, PropertyName = sortingProperty } },
                pageNumber,
                itemsPerPage,
                ActiveTransaction
                );
        }

        public override IEnumerable<TEntity> GetAllPaged(Expression<Func<TEntity, bool>> predicate, 
            int pageNumber, int itemsPerPage, bool ascending = true,
            params Expression<Func<TEntity, object>>[] sortingExpression)
        {
            IPredicate filteredPredicate = DapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>(predicate);
            return Connection.GetPage<TEntity>(filteredPredicate, sortingExpression.ToSortable(ascending),
                pageNumber, itemsPerPage, ActiveTransaction);
        }

        public override IEnumerable<TEntity> GetSet(Expression<Func<TEntity, bool>> predicate,
            int firstResult, int maxResults,string sortingProperty, bool ascending = true)
        {
            IPredicate filteredPredicate = DapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>(predicate);
            return Connection.GetSet<TEntity>(
                filteredPredicate,
                new List<ISort> { new Sort { Ascending=ascending,PropertyName=sortingProperty} },
                firstResult,
                maxResults,
                ActiveTransaction
                );
        }

        public override IEnumerable<TEntity> GetSet(Expression<Func<TEntity, bool>> predicate, int firstResult, 
            int maxResults, bool ascending = true,params Expression<Func<TEntity, object>>[] sortingExpression)
        {
            IPredicate filteredPredicate= DapperQueryFilterExecuter.ExecuteFilter<TEntity,TPrimaryKey>(predicate);
            return Connection.GetSet<TEntity>(filteredPredicate, 
                sortingExpression.ToSortable(ascending), firstResult, maxResults, ActiveTransaction);
        }

        public override void Insert(TEntity entity)
        {
            InsertAndGetId(entity);
        }

        public override TPrimaryKey InsertAndGetId(TEntity entity)
        {
            EntityChangeEventHelper.TriggerEntityCreatingEvent(entity);
            DapperActionFilterExecuter.ExecuteCreationAuditFilter<TEntity, TPrimaryKey>(entity);
            TPrimaryKey primaryKey = Connection.Insert(entity, ActiveTransaction);
            EntityChangeEventHelper.TriggerEntityCreatedEventOnUowCompleted(entity);
            return primaryKey;
        }

        public override IEnumerable<TEntity> Query(string query,object parameters = null)
        {
            return Connection.Query<TEntity>(query, parameters, ActiveTransaction);
        }

        public override IEnumerable<TAny> Query<TAny>(string query,object parameters = null)
        {
            return Connection.Query<TAny>(query, parameters, ActiveTransaction);
        }

        public override TEntity Single(TPrimaryKey id)
        {
            return Single(CreateEqualityExpressionForId(id));
        }

        public override TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            IPredicate pg = DapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>(predicate);
            return Connection.GetList<TEntity>(pg, transaction: ActiveTransaction).Single();
        }

        public override void Update(TEntity entity)
        {
            EntityChangeEventHelper.TriggerEntityUpdatingEvent(entity);
            DapperActionFilterExecuter.ExecuteModificationAuditFilter<TEntity, TPrimaryKey>(entity);
            Connection.Update(entity, ActiveTransaction);
            EntityChangeEventHelper.TriggerEntityUpdatedEventOnUowCompleted(entity);
        }
        protected static Expression<Func<TEntity,bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        {
            ParameterExpression lambdaParam = Expression.Parameter(typeof(TEntity));
            BinaryExpression lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, "Id"),
                Expression.Constant(id, typeof(TPrimaryKey)));
            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }
        #endregion
    }
}
