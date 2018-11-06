using AbpFramework.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace Abp.EntityFramework.EntityFramework
{
    public interface IDbContextProvider<out TDbContext>
        where TDbContext : DbContext
    {
        TDbContext GetDbContext();
        TDbContext GetDbContext(MultiTenancySides? multiTenancySide);
    }
}
