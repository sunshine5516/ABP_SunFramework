using AbpFramework.Dependency;
using AbpFramework.Domain.Repositories;
using System;
namespace Abp.EntityFramework.Common.Repositories
{
    public interface IEfGenericRepositoryRegistrar
    {
       void RegisterForDbContext(Type dbContextType, IIocManager iocManager,
           AutoRepositoryTypesAttribute defaultAutoRepositoryTypesAttribute);
    }
}
