using AbpFramework.Dependency;
using AbpFramework.Domain.Entities;
using DapperExtensions;
using System;
using System.Linq.Expressions;
namespace Abp.Dapper.Dapper.Filters.Query
{
    public interface IDapperQueryFilter: ITransientDependency
    {
        string FilterName { get; }
        bool IsEnabled { get; }
        IFieldPredicate ExecuteFilter<TEntity,TPrimaryKey>()
            where TEntity:class, IEntity<TPrimaryKey>;
        Expression<Func<TEntity, bool>> ExecuteFilter<TEntity, TPrimaryKey>
            (Expression<Func<TEntity, bool>> predicate) where TEntity : class, IEntity<TPrimaryKey>;
    }
}
