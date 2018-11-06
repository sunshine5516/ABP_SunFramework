using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace Abp.EntityFramework.EntityFramework.Repositories
{
    public interface IRepositoryWithDbContext
    {
        DbContext GetDbContext();
    }
}
