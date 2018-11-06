using AbpFramework.Dependency;
using AbpFramework.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Orm
{
    public interface ISecondaryOrmRegistrar
    {
        string OrmContextKey { get; }
        void RegisterRepositories(IIocManager iocManager, AutoRepositoryTypesAttribute defaultRepositoryTypes);
    }
}
