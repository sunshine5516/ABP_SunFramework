using AbpFramework.Data;
using AbpFramework.Domain.Entities;
using AbpFramework.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AbpFramework.Collections.Extensions;
using AbpFramework;

namespace Abp.EntityFramework.EntityFramework.Repositories
{
    /// <summary>
    /// EF存储
    /// </summary>
    /// <typeparam name="TDbContext">DbContext which contains <typeparamref name="TEntity"/>.</typeparam>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TPrimaryKey">主键</typeparam>
    public class EfRepositoryBase<TDbContext, TEntity, TPrimaryKey> : 
        AbpRepositoryBase<TEntity, TPrimaryKey>, IRepositoryWithDbContext,
        ISupportsExplicitLoading<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
        where TDbContext : DbContext
    {
        #region 声明实例
        private readonly IDbContextProvider<TDbContext> _dbContextProvider;
        public virtual TDbContext Context => _dbContextProvider.GetDbContext(MultiTenancySide);
        public virtual DbSet<TEntity> Table => Context.Set<TEntity>();
        public IActiveTransactionProvider TransactionProvider { private get; set; }
        public virtual DbTransaction DbTransaction
        {
            get
            {
                return (DbTransaction)TransactionProvider?.GetActiveTransaction(new ActiveTransactionProviderArgs
                {
                    {"ContextType", typeof(TDbContext) },
                    {"MultiTenancySide", MultiTenancySide }
                });
            }
        }
        public virtual DbConnection Connection
        {
            get
            {
                var connection = Context.Database.Connection;
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                return connection;
            }
        }
        #endregion
        #region 构造函数
        public EfRepositoryBase(IDbContextProvider<TDbContext> dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }
        #endregion
        #region 方法
        /// <summary>
        /// 获取EF上下文对象
        /// </summary>
        /// <returns></returns>
        public virtual DbContext GetDbContext()
        {
            return Context;
        }
        #region 查找

        

        public override IQueryable<TEntity> GetAll()
        {
            return Table;
        }
        public override IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            if (propertySelectors.IsNullOrEmpty())
            {
                return GetAll();
            }
            var query = GetAll();
            foreach (var propertySelector in propertySelectors)
            {
                query = query.Include(propertySelector);
            }
            return query;
        }
        public override async Task<List<TEntity>> GetAllListAsync()
        {
            return await GetAll().ToListAsync();
        }
        public override async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).ToListAsync();
        }
        public override async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().SingleAsync(predicate);
        }
        public override async Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id)
        {
            return await GetAll().FirstOrDefaultAsync(CreateEqualityExpressionForId(id));
        }
        public override async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().FirstOrDefaultAsync(predicate);
        }
        #endregion
        #region 添加
        public override TEntity Insert(TEntity entity)
        {
            return Table.Add(entity);
        }
        public override Task<TEntity> InsertAsync(TEntity entity)
        {           
            return Task.FromResult(Table.Add(entity));
        }
        public override TPrimaryKey InsertAndGetId(TEntity entity)
        {
            entity = Insert(entity);
            if (entity.IsTransient())
            {
                Context.SaveChanges();
            }
            return entity.Id;
        }
        public override async Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity)
        {
            entity = await InsertAsync(entity);
            if (entity.IsTransient())
            {
                await Context.SaveChangesAsync();
            }
            return entity.Id;
        }
        public override TPrimaryKey InsertOrUpdateAndGetId(TEntity entity)
        {
            entity = InsertOrUpdate(entity);
            if (entity.IsTransient())
            {
                Context.SaveChanges();
            }
            return entity.Id;
        }
        public override async Task<TPrimaryKey> InsertOrUpdateAndGetIdAsync(TEntity entity)
        {
            entity = await InsertOrUpdateAsync(entity);

            if (entity.IsTransient())
            {
                await Context.SaveChangesAsync();
            }

            return entity.Id;
        }
        #endregion
        #region 更新
        public override TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);
            Context.Entry(entity).State= EntityState.Modified;
            return entity;
        }
        public override Task<TEntity> UpdateAsync(TEntity entity)
        {
            AttachIfNot(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return Task.FromResult(entity);
        }
        #endregion

        public override void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(TPrimaryKey id)
        {
            throw new NotImplementedException();
        }

        public Task EnsureCollectionLoadedAsync<TProperty>(TEntity entity,
            Expression<Func<TEntity, IEnumerable<TProperty>>> 
            collectionExpression, CancellationToken cancellationToken) where TProperty : class
        {
            var expression = collectionExpression.Body as MemberExpression;
            if (expression == null)
            {
                throw new AbpException($"Given {nameof(collectionExpression)} is not a {typeof(MemberExpression).FullName}");
            }
            return Context.Entry(entity)
                .Collection(expression.Member.Name)
                .LoadAsync(cancellationToken);
        }

        public Task EnsurePropertyLoadedAsync<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> propertyExpression,
           CancellationToken cancellationToken) where TProperty : class
        {
            return Context.Entry(entity).Reference(propertyExpression).LoadAsync(cancellationToken);
        }
        #region 其他
        public override async Task<int> CountAsync()
        {
            return await GetAll().CountAsync();
        }
        public override async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).CountAsync();
        }
        public override async Task<long> LongCountAsync()
        {
            return await GetAll().LongCountAsync();
        }
        public override async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).LongCountAsync();
        }

        protected virtual void AttachIfNot(TEntity entity)
        {
            if (!Table.Local.Contains(entity))
            {
                Table.Attach(entity);
            }
        }
        #endregion

        #endregion

    }
}
