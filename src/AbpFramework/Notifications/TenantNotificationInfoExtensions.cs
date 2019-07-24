using AbpFramework.Domain.Entities;
using AbpFramework.Extensions;
using Newtonsoft.Json;
using System;
namespace AbpFramework.Notifications
{
    public static class TenantNotificationInfoExtensions
    {
        public static TenantNotification ToTenantNotification(this TenantNotificationInfo tenantNotificationInfo)
        {
            var entityType = tenantNotificationInfo.EntityTypeAssemblyQualifiedName.IsNullOrEmpty()
                ? null
                : Type.GetType(tenantNotificationInfo.EntityTypeAssemblyQualifiedName);
            return new TenantNotification
            {
                Id = tenantNotificationInfo.Id,
                TenantId = tenantNotificationInfo.TenantId,
                NotificationName = tenantNotificationInfo.NotificationName,
                Data = tenantNotificationInfo.Data.IsNullOrEmpty() ? null : JsonConvert.DeserializeObject(tenantNotificationInfo.Data, Type.GetType(tenantNotificationInfo.DataTypeName)) as NotificationData,
                EntityTypeName = tenantNotificationInfo.EntityTypeName,
                EntityType = entityType,
                EntityId = tenantNotificationInfo.EntityId.IsNullOrEmpty() ? null : JsonConvert.DeserializeObject(tenantNotificationInfo.EntityId, EntityHelper.GetPrimaryKeyType(entityType)),
                Severity = tenantNotificationInfo.Severity,
                CreationTime = tenantNotificationInfo.CreationTime
            };
        }
    }
}
