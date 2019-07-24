﻿using AbpFramework.Domain.Entities;
namespace Abp.Dapper.Dapper.Repositories
{
    public interface IDapperRepository<TEntity> : IDapperRepository<TEntity, int>
        where TEntity : class, IEntity<int>
    {
    }
}
