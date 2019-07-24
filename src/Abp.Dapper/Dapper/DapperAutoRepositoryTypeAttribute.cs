using AbpFramework.Domain.Repositories;
using JetBrains.Annotations;
using System;
namespace Abp.Dapper.Dapper
{
    public class DapperAutoRepositoryTypeAttribute: AutoRepositoryTypesAttribute
    {
        public DapperAutoRepositoryTypeAttribute(
            [NotNull] Type repositoryInterface,
            [NotNull] Type repositoryInterfaceWithPrimaryKey,
            [NotNull] Type repositoryImplementation,
            [NotNull] Type repositoryImplementationWithPrimaryKey)
            :base(repositoryInterface, repositoryInterfaceWithPrimaryKey, 
                 repositoryImplementation, repositoryImplementationWithPrimaryKey)
        {

        }
    }
}
