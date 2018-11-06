using AbpFramework.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace Abp.EntityFramework.EntityFramework.Uow
{
    public interface IEfUnitOfWorkFilterExecuter : IUnitOfWorkFilterExecuter
    {
        void ApplyCurrentFilters(IUnitOfWork unitOfWork, DbContext dbContext);
    }
}
