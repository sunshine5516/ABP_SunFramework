using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Text;

namespace Abp.EntityFramework.EntityFramework
{
    public interface IDbContextResolver
    {
        TDbContext Resolve<TDbContext>(string connectionString)
            where TDbContext : DbContext;
        TDbContext Resolve<TDbContext>(DbConnection existingConnection, bool contextOwnsConnection)
           where TDbContext : DbContext;
    }
}
