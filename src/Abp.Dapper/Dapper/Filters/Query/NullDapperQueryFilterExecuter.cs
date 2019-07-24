using System;
using System.Linq.Expressions;
using AbpFramework.Domain.Entities;
using DapperExtensions;
namespace Abp.Dapper.Dapper.Filters.Query
{
    public class NullDapperQueryFilterExecuter:IDapperQueryFilterExecuter
    {
        public static readonly NullDapperQueryFilterExecuter Instance = new NullDapperQueryFilterExecuter();

        public IPredicate ExecuteFilter<TEntity, TPrimaryKey>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class, IEntity<TPrimaryKey>
        {
            return null;
        }

        public PredicateGroup ExecuteFilter<TEntity, TPrimaryKey>()
            where TEntity : class, IEntity<TPrimaryKey>
        {
            return null;
        }
    }
}
