using AbpFramework.Domain.Entities;
using AbpFramework.Extensions;
using Newtonsoft.Json;
using System;
namespace AbpFramework.Notifications
{
    public static class NotificationSubscriptionInfoExtensions
    {
        public static NotificationSubscription ToNotificationSubscription
            (this NotificationSubscriptionInfo subscriptionInfo)
        {
            var entityType = subscriptionInfo.EntityTypeAssemblyQualifiedName.IsNullOrEmpty()
                ?null 
                : Type.GetType(subscriptionInfo.EntityTypeAssemblyQualifiedName);
            return new NotificationSubscription
            {
                TenantId = subscriptionInfo.TenantId,
                UserId = subscriptionInfo.UserId,
                NotificationName = subscriptionInfo.NotificationName,
                EntityType = entityType,
                EntityTypeName = subscriptionInfo.EntityTypeName,
                EntityId = subscriptionInfo.EntityId.IsNullOrEmpty() ? null 
                : JsonConvert.DeserializeObject(subscriptionInfo.EntityId, EntityHelper.GetPrimaryKeyType(entityType)),
                CreationTime = subscriptionInfo.CreationTime
            };
        }
    }
}
