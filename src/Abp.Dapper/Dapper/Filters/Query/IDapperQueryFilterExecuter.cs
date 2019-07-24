using AbpFramework.Domain.Entities;
using DapperExtensions;
using System;
using System.Linq.Expressions;
namespace Abp.Dapper.Dapper.Filters.Query
{
    public interface IDapperQueryFilterExecuter
    {
        IPredicate ExecuteFilter<TEntity, TPrimaryKey>(Expression<Func<TEntity, bool>> predicate) 
            where TEntity : class, IEntity<TPrimaryKey>;

        PredicateGroup ExecuteFilter<TEntity, TPrimaryKey>()
            where TEntity : class, IEntity<TPrimaryKey>;
    }
}
