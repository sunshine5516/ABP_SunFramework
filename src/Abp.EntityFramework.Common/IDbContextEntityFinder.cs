using AbpFramework.Domain.Entities;
using System;
using System.Collections.Generic;
namespace Abp.EntityFramework.Common
{
    public interface IDbContextEntityFinder
    {
        IEnumerable<EntityTypeInfo> GetEntityTypeInfos(Type dbContextType);
    }
}
