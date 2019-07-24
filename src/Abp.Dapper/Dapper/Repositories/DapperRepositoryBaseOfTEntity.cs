﻿using AbpFramework.Data;
using AbpFramework.Domain.Entities;
namespace Abp.Dapper.Dapper.Repositories
{
    public class DapperRepositoryBase<TEntity>:DapperRepositoryBase<TEntity,int>, IDapperRepository<TEntity>
        where TEntity:class, IEntity<int>
    {
        public DapperRepositoryBase(IActiveTransactionProvider activeTransactionProvider) 
            : base(activeTransactionProvider)
        {

        }
    }
}
