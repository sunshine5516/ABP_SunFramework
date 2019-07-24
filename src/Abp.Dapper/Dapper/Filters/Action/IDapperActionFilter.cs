﻿using AbpFramework.Dependency;
using AbpFramework.Domain.Entities;
namespace Abp.Dapper.Dapper.Filters.Action
{
    public interface IDapperActionFilter: ITransientDependency
    {
        void ExecuteFilter<TEntity,TPrimaryKey>(TEntity entity) 
            where TEntity : class, IEntity<TPrimaryKey>;
    }
}
