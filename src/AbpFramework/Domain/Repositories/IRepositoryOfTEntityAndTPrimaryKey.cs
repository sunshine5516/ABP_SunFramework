using AbpFramework.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AbpFramework.Domain.Repositories
{
   public interface IRepository<TEntity, TPrimaryKey> : 
        IRepository where TEntity : class, IEntity<TPrimaryKey>
    {
        #region 查询
        /// <summary>
        /// 获取用于从整个表中检索实体的IQueryable
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetAll();
        /// <summary>
        /// 获取用于从整个表中检索实体的IQueryable
        /// </summary>
        /// <param name="propertySelectors">表达式的列表.</param>
        /// <returns></returns>
        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors);
        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <returns>实体集合</returns>
        List<TEntity> GetAllList();

        /// <summary>
        /// 根据条件过滤
        /// </summary>
        /// <param name="predicate">过滤条件</param>
        /// <returns>实体集合</returns>
        List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 根据条件过滤（异步）
        /// </summary>
        /// <param name="predicate">过滤条件</param>
        /// <returns>实体集合</returns>
        Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 对整个实体运行查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryMethod"></param>
        /// <returns></returns>
        T Query<T>(Func<IQueryable<TEntity>, T> queryMethod);
        /// <summary>
        /// 根据ID获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Get(TPrimaryKey id);
        /// <summary>
        /// 根据ID获取实体(异步)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync(TPrimaryKey id);
        /// <summary>
        /// 获取具有给定谓词的一个实体。
        /// 如果没有实体或者不止一个，则抛出异常
        /// </summary>
        /// <param name="predicate">Entity</param>
        TEntity Single(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 获取具有给定谓词的一个实体。（异步）
        /// 如果没有实体或者不止一个，则抛出异常
        /// </summary>
        /// <param name="predicate">Entity</param>
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 根据ID获取实体或者返回空
        /// </summary>
        /// <param name="id">实体ID</param>
        /// <returns>Entity or null</returns>
        TEntity FirstOrDefault(TPrimaryKey id);

        /// <summary>
        /// 根据ID获取实体或者返回空（异步)
        /// </summary>
        /// <param name="id">实体ID</param>
        /// <returns>Entity or null</returns>
        Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id);
        /// <summary>
        /// 根据指定条件获取实体或者返回空
        /// </summary>
        /// <param name="predicate">过滤条件</param>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 根据指定条件获取实体或者返回空（异步）
        /// </summary>
        /// <param name="predicate">过滤条件</param>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 给定主键创建没有数据库访问权限的实体.
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>Entity</returns>
        TEntity Load(TPrimaryKey id);
        #endregion
        #region 添加
        /// <summary>
        /// 添加新的实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Insert(TEntity entity);
        /// <summary>
        /// 添加新的实体（异步）
        /// </summary>
        /// <param name="entity">添加的实体</param>
        Task<TEntity> InsertAsync(TEntity entity);
        /// <summary>
        /// 添加新的实体并返回ID
        /// It may require to save current unit of work
        /// to be able to retrieve id.
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>实体ID</returns>
        TPrimaryKey InsertAndGetId(TEntity entity);
        /// <summary>
        ///添加新的实体并返回ID（异步）
        /// It may require to save current unit of work
        /// to be able to retrieve id.
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>实体ID</returns>
        Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity);
        /// <summary>
        /// 根据Id的值插入或更新给定实体。
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity InsertOrUpdate(TEntity entity);
        /// <summary>
        /// 根据Id的值插入或更新给定实体。（异步）
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task<TEntity> InsertOrUpdateAsync(TEntity entity);
        /// <summary>
        ///添加或更新实体并返回ID
        /// It may require to save current unit of work
        /// to be able to retrieve id.
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>实体ID</returns>
        /// <returns></returns>
        TPrimaryKey InsertOrUpdateAndGetId(TEntity entity);
        /// <summary>
        /// 添加或更新实体并返回ID（异步）
        /// It may require to save current unit of work
        /// to be able to retrieve id.
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>实体ID</returns>
        Task<TPrimaryKey> InsertOrUpdateAndGetIdAsync(TEntity entity);
        #endregion
        #region 更新
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Update(TEntity entity);
        /// <summary>
        /// 更新已经存在的实体(异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// 更新已经存在的实体
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="updateAction">用于更改实体值的操作</param>
        /// <returns>Updated entity</returns>
        TEntity Update(TPrimaryKey id, Action<TEntity> updateAction);

        /// <summary>
        /// 更新已经存在的实体（异步）
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="updateAction">用于更改实体值的操作</param>
        /// <returns>Updated entity</returns>
        Task<TEntity> UpdateAsync(TPrimaryKey id, Func<TEntity, Task> updateAction);

        #endregion
        #region 删除
        /// <summary>
        /// 删除一个实体
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);
        /// <summary>
        /// 删除一个实体(异步)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(TEntity entity);
        /// <summary>
        /// 根据ID删除实体
        /// </summary>
        /// <param name="id"></param>
        void Delete(TPrimaryKey id);
        /// <summary>
        /// 根据ID删除实体(异步）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(TPrimaryKey id);
        void Delete(Expression<Func<TEntity, bool>> predicate);
        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);
        #endregion
        #region 合计操作
        /// <summary>
        /// 获取此存储库中所有实体的计数。
        /// </summary>
        /// <returns>实体数量</returns>
        int Count();

        /// <summary>
        /// 获取此存储库中所有实体的计数（异步）.
        /// </summary>
        /// <returns>实体数量</returns>
        Task<int> CountAsync();
        /// <summary>
        /// 根据指定条件获取此存储库中所有实体的计数
        /// </summary>
        /// <param name="predicate">过滤条件</param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 根据指定条件获取此存储库中所有实体的计数（异步）
        /// </summary>
        /// <param name="predicate">过滤条件</param>
        /// <returns>Count of entities</returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);


        /// <summary>
        /// Gets count of all entities in this repository (use if expected return value is greather than <see cref="int.MaxValue"/>.
        /// </summary>
        /// <returns>Count of entities</returns>
        long LongCount();

        /// <summary>
        /// Gets count of all entities in this repository (use if expected return value is greather than <see cref="int.MaxValue"/>.
        /// </summary>
        /// <returns>Count of entities</returns>
        Task<long> LongCountAsync();

        /// <summary>
        /// Gets count of all entities in this repository based on given <paramref name="predicate"/>
        /// (use this overload if expected return value is greather than <see cref="int.MaxValue"/>).
        /// </summary>
        /// <param name="predicate">A method to filter count</param>
        /// <returns>Count of entities</returns>
        long LongCount(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets count of all entities in this repository based on given <paramref name="predicate"/>
        /// (use this overload if expected return value is greather than <see cref="int.MaxValue"/>).
        /// </summary>
        /// <param name="predicate">A method to filter count</param>
        /// <returns>Count of entities</returns>
        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate);

        #endregion
    }
}
