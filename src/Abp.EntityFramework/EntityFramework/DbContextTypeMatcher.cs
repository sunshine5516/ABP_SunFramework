using Abp.EntityFramework.Common;
using AbpFramework.Domain.Uow;

namespace Abp.EntityFramework.EntityFramework
{
    public class DbContextTypeMatcher : DbContextTypeMatcher<AbpDbContext>
    {
        public DbContextTypeMatcher(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider)
           : base(currentUnitOfWorkProvider)
        {
        }
    }
}
