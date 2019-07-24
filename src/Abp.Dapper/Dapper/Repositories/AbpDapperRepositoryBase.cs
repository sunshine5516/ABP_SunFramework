using AbpFramework.Domain.Entities;
using AbpFramework.MultiTenancy;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AbpFramework.Reflection.Extensions;
namespace Abp.Dapper.Dapper.Repositories
{
    /// <summary>
    /// <see cref="IDapperRepository{TEntity,TPrimaryKey}" />基类.
    /// 它以最简单的方式实现了一些方法.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public abstract class AbpDapperRepositoryBase<TEntity, TPrimaryKey>
        : IDapperRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        #region 声明实例
        public static MultiTenancySides? MultiTenancySide { get; private set; }
        #endregion
        #region 构造函数
        static AbpDapperRepositoryBase()
        {
            var attr=typeof(TEntity).GetSingleAttributeOfTypeOrBaseTypesOrNull<MultiTenancySideAttribute>();
            if(attr!=null)
            {
                MultiTenancySide = attr.Side;
            }
        }
        #endregion
        #region 方法

        public abstract int Count([NotNull] Expression<Func<TEntity, bool>> predicate);
        public virtual Task<int> CountAsync([NotNull] Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(Count(predicate));
        }

        public abstract void Delete([NotNull] TEntity entity);

        public abstract void Delete([NotNull] Expression<Func<TEntity, bool>> predicate);
        public virtual Task DeleteAsync([NotNull] TEntity entity)
        {
            Delete(entity);
            return Task.FromResult(0);
        }

        public Task DeleteAsync([NotNull] Expression<Func<TEntity, bool>> predicate)
        {
            Delete(predicate);
            return Task.FromResult(0);
        }

        public abstract int Execute(string query, object parameters = null);

        public virtual Task<int> ExecuteAsync([NotNull] string query, [CanBeNull] object parameters = null)
        {
            return Task.FromResult(Execute(query, parameters));
        }

        public abstract TEntity FirstOrDefault([NotNull] TPrimaryKey id);

        public abstract TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        public virtual Task<TEntity> FirstOrDefaultAsync([NotNull] TPrimaryKey id)
        {
            return Task.FromResult(FirstOrDefault(id));
        }

        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(FirstOrDefault(predicate));
        }

        public abstract TEntity Get([NotNull] TPrimaryKey id);

        public abstract IEnumerable<TEntity> GetAll();

        public abstract IEnumerable<TEntity> GetAll([NotNull] Expression<Func<TEntity, bool>> predicate);

        public virtual Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return Task.FromResult(GetAll());
        }

        public virtual Task<IEnumerable<TEntity>> GetAllAsync([NotNull] Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(GetAll(predicate));
        }

        public abstract IEnumerable<TEntity> GetAllPaged([NotNull] Expression<Func<TEntity, bool>> predicate,
            int pageNumber, int itemsPerPage, [NotNull] string sortingProperty, bool ascending = true);

        public abstract IEnumerable<TEntity> GetAllPaged([NotNull] Expression<Func<TEntity, bool>> predicate,
            int pageNumber, int itemsPerPage, bool ascending = true,
            [NotNull] params Expression<Func<TEntity, object>>[] sortingExpression);

        public virtual Task<IEnumerable<TEntity>> GetAllPagedAsync([NotNull] Expression<Func<TEntity, bool>> predicate, 
            int pageNumber, int itemsPerPage, [NotNull] string sortingProperty, bool ascending = true)
        {
            return Task.FromResult(GetAllPaged(predicate, pageNumber, itemsPerPage, sortingProperty, ascending));
        }

        public virtual Task<IEnumerable<TEntity>> GetAllPagedAsync([NotNull] Expression<Func<TEntity, bool>> predicate, 
            int pageNumber, int itemsPerPage, bool ascending = true, 
            [NotNull] params Expression<Func<TEntity, object>>[] sortingExpression)
        {
            return Task.FromResult(GetAllPaged(predicate, pageNumber, itemsPerPage, ascending, sortingExpression));
        }

        public virtual Task<TEntity> GetAsync([NotNull] TPrimaryKey id)
        {
            return Task.FromResult(Get(id));
        }

        public abstract IEnumerable<TEntity> GetSet([NotNull] Expression<Func<TEntity, bool>> predicate,
            int firstResult, int maxResults, [NotNull] string sortingProperty, bool ascending = true);

        public abstract IEnumerable<TEntity> GetSet([NotNull] Expression<Func<TEntity, bool>> predicate,
            int firstResult, int maxResults, bool ascending = true, [NotNull] params Expression<Func<TEntity, object>>[] sortingExpression);

        public virtual Task<IEnumerable<TEntity>> GetSetAsync([NotNull] Expression<Func<TEntity, bool>> predicate, 
            int firstResult, int maxResults, [NotNull] string sortingProperty, bool ascending = true)
        {
            return Task.FromResult(GetSet(predicate, firstResult, maxResults, sortingProperty, ascending));
        }

        public virtual Task<IEnumerable<TEntity>> GetSetAsync([NotNull] Expression<Func<TEntity, bool>> predicate,
            int firstResult, int maxResults, bool ascending = true, [NotNull] params Expression<Func<TEntity, object>>[] sortingExpression)
        {
            return Task.FromResult(GetSet(predicate, firstResult, maxResults, ascending, sortingExpression));
        }

        public abstract void Insert([NotNull] TEntity entity);

        public abstract TPrimaryKey InsertAndGetId([NotNull] TEntity entity);

        public virtual Task<TPrimaryKey> InsertAndGetIdAsync([NotNull] TEntity entity)
        {
            return Task.FromResult(InsertAndGetId(entity));
        }

        public virtual Task InsertAsync([NotNull] TEntity entity)
        {
            Insert(entity);
            return Task.FromResult(0);
        }

        public abstract IEnumerable<TEntity> Query([NotNull] string query, [CanBeNull] object parameters = null);

        public abstract IEnumerable<TAny> Query<TAny>([NotNull] string query, [CanBeNull] object parameters = null)
            where TAny : class;

        public virtual Task<IEnumerable<TEntity>> QueryAsync(string query,object parameters = null)
        {
            return Task.FromResult(Query(query, parameters));
        }

        public virtual Task<IEnumerable<TAny>> QueryAsync<TAny>(string query,object parameters = null)
            where TAny : class
        {
            return Task.FromResult(Query<TAny>(query, parameters));
        }

        public abstract TEntity Single([NotNull] TPrimaryKey id);

        public abstract TEntity Single(Expression<Func<TEntity, bool>> predicate);

        public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(Single(predicate));
        }

        public virtual Task<TEntity> SingleAsync([NotNull] TPrimaryKey id)
        {
            return Task.FromResult(Single(id));
        }

        public abstract void Update(TEntity entity);

        public virtual Task UpdateAsync(TEntity entity)
        {
            Update(entity);
            return Task.FromResult(0);
        }
        #endregion
    }
}
