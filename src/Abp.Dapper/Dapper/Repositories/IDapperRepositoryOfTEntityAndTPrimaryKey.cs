using AbpFramework.Domain.Entities;
using AbpFramework.Domain.Repositories;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace Abp.Dapper.Dapper.Repositories
{
    /// <summary>
    /// Dapper存储库抽象接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TPrimaryKey">主键</typeparam>
    public interface IDapperRepository<TEntity, TPrimaryKey> : IRepository
        where TEntity : class, IEntity<TPrimaryKey>
    {
        #region 查询
        /// <summary>
        /// 根据ID获取指定实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [NotNull]
        TEntity Single([NotNull] TPrimaryKey id);
        /// <summary>
        /// 根据条件获取指定实体
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        [NotNull]
        TEntity Single(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 根据条件异步获取指定实体
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 根据ID异步获取指定实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [NotNull]
        Task<TEntity> SingleAsync([NotNull] TPrimaryKey id);
        /// <summary>
        /// 根据ID获取指定实体.
        /// </summary>
        /// <param name="id">ID.</param>
        /// <returns></returns>
        [NotNull]
        TEntity Get([NotNull] TPrimaryKey id);
        /// <summary>
        /// 根据ID异步获取指定实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [NotNull]
        Task<TEntity> GetAsync([NotNull] TPrimaryKey id);
        /// <summary>
        /// 根据ID返回第一个查询的实体或默认值
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CanBeNull]
        TEntity FirstOrDefault([NotNull] TPrimaryKey id);
        /// <summary>
        /// 根据ID异步返回第一个查询的实体或默认值.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [CanBeNull]
        Task<TEntity> FirstOrDefaultAsync([NotNull] TPrimaryKey id);
        /// <summary>
        /// 根据条件返回第一个查询的实体或默认值
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        [CanBeNull]
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 根据条件异步返回第一个查询的实体或默认值
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        [CanBeNull]
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 获取所有值.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        IEnumerable<TEntity> GetAll();
        /// <summary>
        /// 异步获取所有值.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        Task<IEnumerable<TEntity>> GetAllAsync();
        /// <summary>
        /// 根据条件获取所有集合
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        [NotNull]
        IEnumerable<TEntity> GetAll([NotNull] Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 根据条件异步获取所有集合.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        [NotNull]
        Task<IEnumerable<TEntity>> GetAllAsync([NotNull] Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 异步获取分页的列表
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pageNumber"></param>
        /// <param name="itemsPerPage"></param>
        /// <param name="sortingProperty"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        [NotNull]
        Task<IEnumerable<TEntity>> GetAllPagedAsync([NotNull] Expression<Func<TEntity, bool>> predicate,
            int pageNumber, int itemsPerPage, [NotNull] string sortingProperty, bool ascending = true);
        /// <summary>
        /// 异步获取分页的列表
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pageNumber"></param>
        /// <param name="itemsPerPage"></param>
        /// <param name="ascending"></param>
        /// <param name="sortingExpression"></param>
        /// <returns></returns>
        [NotNull]
        Task<IEnumerable<TEntity>>GetAllPagedAsync([NotNull] Expression<Func<TEntity,bool>> predicate,
            int pageNumber,int itemsPerPage,bool ascending=true,
            [NotNull] params Expression<Func<TEntity,object>>[] sortingExpression);
        /// <summary>
        /// 获取分页的列表.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="itemsPerPage">The items per page.</param>
        /// <param name="sortingProperty">The sorting property.</param>
        /// <param name="ascending">if set to <c>true</c> [ascending].</param>
        /// <returns></returns>
        [NotNull]
        IEnumerable<TEntity> GetAllPaged([NotNull] Expression<Func<TEntity, bool>> predicate, int pageNumber, int itemsPerPage, [NotNull] string sortingProperty, bool ascending = true);

        /// <summary>
        /// 获取分页的列表.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="itemsPerPage">The items per page.</param>
        /// <param name="sortingExpression">The sorting expression.</param>
        /// <param name="ascending">if set to <c>true</c> [ascending].</param>
        /// <returns></returns>
        [NotNull]
        IEnumerable<TEntity> GetAllPaged([NotNull] Expression<Func<TEntity, bool>> predicate, int pageNumber, int itemsPerPage, bool ascending = true, [NotNull] params Expression<Func<TEntity, object>>[] sortingExpression);
        /// <summary>
        /// 返回满足条件的个数
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Count([NotNull] Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 异步获取返回满足条件的个数.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        [NotNull]
        Task<int> CountAsync([NotNull] Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 查询指定的查询
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [NotNull]
        IEnumerable<TEntity> Query([NotNull]string query, [CanBeNull] object parameters = null);
        /// <summary>
        /// 异步查询指定的查询.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        [NotNull]
        Task<IEnumerable<TEntity>> QueryAsync([NotNull]string query, [CanBeNull]object parameters = null);
        /// <summary>
        /// 查询指定的查询
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        IEnumerable<TAny> Query<TAny>([NotNull] string query,[CanBeNull]object parameters = null) 
            where TAny : class;
        /// <summary>
        /// 异步查询指定的查询.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        [NotNull]
        Task<IEnumerable<TAny>> QueryAsync<TAny>([NotNull] string query, [CanBeNull] object parameters = null)
            where TAny : class;
        /// <summary>
        /// 执行给定的查询文本
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int Execute([NotNull] string query, [CanBeNull]object parameters = null);
        /// <summary>
        /// 异步执行给定的查询文本
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        Task<int> ExecuteAsync([NotNull] string query, [CanBeNull] object parameters = null);
        /// <summary>
        ///  获取集合.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="firstResult">The first result.</param>
        /// <param name="maxResults">The maximum results.</param>
        /// <param name="sortingProperty">The sorting property.</param>
        /// <param name="ascending">if set to <c>true</c> [ascending].</param>
        /// <returns></returns>
        [NotNull]
        IEnumerable<TEntity> GetSet([NotNull] Expression<Func<TEntity, bool>> predicate, 
            int firstResult, int maxResults, [NotNull] string sortingProperty, bool ascending = true);
        /// <summary>
        /// 获取集合.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="firstResult">The first result.</param>
        /// <param name="maxResults">The maximum results.</param>
        /// <param name="sortingExpression">The sorting expression.</param>
        /// <param name="ascending">if set to <c>true</c> [ascending].</param>
        /// <returns></returns>
        [NotNull]
        IEnumerable<TEntity> GetSet([NotNull] Expression<Func<TEntity, bool>> predicate, int firstResult,
            int maxResults, bool ascending = true, [NotNull] params Expression<Func<TEntity, object>>[] sortingExpression);

        /// <summary>
        /// 异步获取集合.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="firstResult">The first result.</param>
        /// <param name="maxResults">The maximum results.</param>
        /// <param name="sortingProperty">The sorting property.</param>
        /// <param name="ascending">if set to <c>true</c> [ascending].</param>
        /// <returns></returns>
        [NotNull]
        Task<IEnumerable<TEntity>> GetSetAsync([NotNull] Expression<Func<TEntity, bool>> predicate,
            int firstResult, int maxResults, [NotNull] string sortingProperty, bool ascending = true);

        /// <summary>
        /// 异步获取集合.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="firstResult">The first result.</param>
        /// <param name="maxResults">The maximum results.</param>
        /// <param name="ascending">if set to <c>true</c> [ascending].</param>
        /// <param name="sortingExpression">The sorting expression.</param>
        /// <returns></returns>
        [NotNull]
        Task<IEnumerable<TEntity>> GetSetAsync([NotNull] Expression<Func<TEntity, bool>> predicate,
            int firstResult, int maxResults, bool ascending = true,
            [NotNull] params Expression<Func<TEntity, object>>[] sortingExpression);

        #endregion
        #region Insert
        void Insert([NotNull] TEntity entity);
        /// <summary>
        /// 添加数据并返回ID
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TPrimaryKey InsertAndGetId([NotNull] TEntity entity);
        [NotNull]
        Task InsertAsync([NotNull] TEntity entity);
        [NotNull]
        Task<TPrimaryKey> InsertAndGetIdAsync([NotNull] TEntity entity);
        #endregion
        #region Update
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        void Update([NotNull] TEntity entity);
        /// <summary>
        /// 异步更新实体.
        /// </summary>
        /// <param name="entity">The entity.</param>
        [NotNull]
        Task UpdateAsync([NotNull] TEntity entity);
        #endregion
        #region Delete
        /// <summary>
        /// 删除实体.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete([NotNull] TEntity entity);

        /// <summary>
        /// 删除实体.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        void Delete([NotNull] Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 异步删除实体.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        [NotNull]
        Task DeleteAsync([NotNull] TEntity entity);

        /// <summary>
        /// 异步删除实体.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        [NotNull]
        Task DeleteAsync([NotNull] Expression<Func<TEntity, bool>> predicate);
        #endregion
    }
}
