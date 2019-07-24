using AbpFramework.Application.Services.Dto;
using AbpFramework.Domain.Entities.Auditing;
using System;
namespace AbpFramework.Notifications
{
    /// <summary>
    /// 表示租户/用户的已发布通知。
    /// </summary>
    [Serializable]
    public class TenantNotification : EntityDto<Guid>, IHasCreationTime
    {
        public int? TenantId { get; set; }
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 唯一名称.
        /// </summary>
        public string NotificationName { get; set; }

        /// <summary>
        /// 通知数据.
        /// </summary>
        public NotificationData Data { get; set; }

        /// <summary>
        /// 试题类型.
        /// </summary>
        public Type EntityType { get; set; }

        /// <summary>
        /// 实体类型名称.
        /// </summary>
        public string EntityTypeName { get; set; }

        /// <summary>
        /// Entity id.
        /// </summary>
        public object EntityId { get; set; }
        /// <summary>
        /// 等级
        /// </summary>
        public NotificationSeverity Severity { get; set; }
        public TenantNotification()
        {
            CreationTime = DateTime.Now;
        }
    }
}
