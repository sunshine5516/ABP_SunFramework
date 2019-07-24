using AbpFramework.Domain.Entities;
using AbpFramework.Domain.Entities.Auditing;
using System;
using AbpFramework.Extensions;
namespace Abp.Dapper.Dapper.Filters.Action
{
    public class ModificationAuditDapperActionFilter : DapperActionFilterBase, IDapperActionFilter
    {
        public void ExecuteFilter<TEntity, TPrimaryKey>(TEntity entity)
            where TEntity : class, IEntity<TPrimaryKey>
        {
            if(entity is IHasModificationTime)
            {
                entity.As<IHasModificationTime>().LastModificationTime = DateTime.Now;
            }
            if (entity is IModificationAudited)
            {
                var record = entity.As<IModificationAudited>();
                long? userId = GetAuditUserId();
                if(userId==null)
                {
                    record.LastModificationTime = null;
                    return;
                }
                if(entity is IMayHaveTenant||entity is IMustHaveTenant)
                {
                    if (entity is IMayHaveTenant && entity.As<IMayHaveTenant>().TenantId == AbpSession.TenantId ||
                                            entity is IMustHaveTenant && entity.As<IMustHaveTenant>().TenantId == AbpSession.TenantId)
                    {
                        record.LastModifierUserId = userId;
                    }
                    else
                    {
                        record.LastModifierUserId = null;
                    }
                }
                else
                {
                    record.LastModifierUserId = userId;
                }
            }
        }
    }
}
