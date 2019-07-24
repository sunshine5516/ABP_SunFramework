using AbpFramework;
using AbpFramework.Domain.Entities;
using DapperExtensions;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Abp.Dapper.Dapper.Extensions
{
    internal static class DapperExpressionExtensions
    {
        //[NotNull]
        //public static IPredicate ToPredicateGroup<TEntity, TPrimaryKey>
        //    ([NotNull] this Expression<Func<TEntity, bool>> expression) 
        //    where TEntity : class, IEntity<TPrimaryKey>
        //{
        //    Check.NotNull(expression, nameof(expression));
        //    var dev = new DapperExpressionVisitor<TEntity, TPrimaryKey>();
        //    IPredicate pg= dev.Process(expression);

        //    return pg;
        //}
    }
}
