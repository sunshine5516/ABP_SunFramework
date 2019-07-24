using AbpFramework;
using AbpFramework.Domain.Entities;
using AbpFramework.Domain.Uow;
using AbpFramework.Reflection;
using AbpFramework.Runtime.Session;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
namespace Abp.Dapper.Dapper.Filters.Action
{
    public abstract class DapperActionFilterBase
    {
        public IAbpSession AbpSession { get; set; }
        public ICurrentUnitOfWorkProvider CurrentUnitOfWorkProvider { get; set; }
        public IGuidGenerator GuidGenerator { get; set; }
        protected DapperActionFilterBase()
        {
            AbpSession = NullAbpSession.Instance;
            GuidGenerator = SequentialGuidGenerator.Instance;
        }
        protected virtual long? GetAuditUserId()
        {
            if(AbpSession.UserId.HasValue&&CurrentUnitOfWorkProvider?.Current!=null)
            {
                return AbpSession.UserId;
            }
            return null;
        }
        protected virtual void CheckAndSetId(object entityAsObj)
        {
            var entity = entityAsObj as IEntity<Guid>;
            if (entity != null && entity.Id == Guid.Empty)
            {
                Type entityType = entityAsObj.GetType();
                PropertyInfo idProperty = entityType.GetProperty("Id");
                var dbGeneratedAttr = ReflectionHelper.GetSingleAttributeOrDefault<DatabaseGeneratedAttribute>(idProperty);
                if (dbGeneratedAttr == null || dbGeneratedAttr.DatabaseGeneratedOption == DatabaseGeneratedOption.None)
                {
                    entity.Id = GuidGenerator.Create();
                }
            }
        }
        protected virtual int? GetCurrentTenantIdOrNull()
        {
            if(CurrentUnitOfWorkProvider?.Current!=null)
            {
                return CurrentUnitOfWorkProvider.Current.GetTenantId();
            }
            return AbpSession.TenantId;
        }
    }
}
