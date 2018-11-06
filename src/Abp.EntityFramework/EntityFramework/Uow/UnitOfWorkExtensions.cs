using AbpFramework.Domain.Uow;
using AbpFramework.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.EntityFramework.EntityFramework.Uow
{
    /// <summary>
    /// UOW扩展方法
    /// </summary>
    public static class UnitOfWorkExtensions
    {
        public static TDbContext GetDbContext<TDbContext>(this IActiveUnitOfWork unitOfWork, MultiTenancySides? multiTenancySide)
            where TDbContext : DbContext
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork));
            }

            if (!(unitOfWork is EfUnitOfWork))
            {
                throw new ArgumentException("unitOfWork is not type of " + typeof(EfUnitOfWork).FullName, nameof(unitOfWork));
            }
            return (unitOfWork as EfUnitOfWork).GetOrCreateDbContext<TDbContext>(multiTenancySide);
        }
    }
}
