using AbpFramework.Domain.Entities.Auditing;
using System;
namespace AbpFramework.Notifications
{
    /// <summary>
    /// 用于封装封装notification 和subscriptor（User） 的关系的DTO,不是Entity。
    /// </summary>
    public class NotificationSubscription : IHasCreationTime
    {
        public int? TenantId { get; set; }
        public long UserId { get; set; }
        public string NotificationName { get; set; }
        public Type EntityType { get; set; }
        public string EntityTypeName { get; set; }
        public object EntityId { get; set; }

        public DateTime CreationTime { get; set; }
        public NotificationSubscription()
        {
            CreationTime = DateTime.Now;
        }
    }
}
