using AbpFramework.Domain.Entities.Auditing;
using AbpFramework.MultiTenancy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Notifications
{
    /// <summary>
    /// 用于封装Notification的Entity.
    /// </summary>
    [Serializable]
    [Table("AbpNotifications")]
    [MultiTenancySide(MultiTenancySides.Host)]
    public class NotificationInfo : CreationAuditedEntity<Guid>
    {
        #region 常量
        /// <summary>
        /// 标识所有租户ID
        /// </summary>
        public const string AllTenantIds = "0";
        /// <summary>
        /// <see cref="NotificationName"/>最大长度.
        /// Value: 96.
        /// </summary>
        public const int MaxNotificationNameLength = 96;
        /// <summary>
        /// <see cref="Data"/>最大长度.
        /// Value: 1048576 (1 MB).
        /// </summary>
        public const int MaxDataLength = 1024 * 1024;
        /// <summary>
        /// <see cref="DataTypeName"/>最大长度.
        /// Value: 512.
        /// </summary>
        public const int MaxDataTypeNameLength = 512;
        /// <summary>
        /// <see cref="EntityTypeName"/>最大长度.
        /// Value: 250.
        /// </summary>
        public const int MaxEntityTypeNameLength = 250;
        /// <summary>
        /// <see cref="EntityTypeAssemblyQualifiedName"/>最大长度.
        /// Value: 512.
        /// </summary>
        public const int MaxEntityTypeAssemblyQualifiedNameLength = 512;

        /// <summary>
        /// <see cref="EntityId"/>最大长度.
        /// Value: 96.
        /// </summary>
        public const int MaxEntityIdLength = 96;

        /// <summary>
        /// <see cref="UserIds"/>最大长度.
        /// Value: 131072 (128 KB).
        /// </summary>
        public const int MaxUserIdsLength = 128 * 1024;

        /// <summary>
        /// <see cref="TenantIds"/>最大长度.
        /// Value: 131072 (128 KB).
        /// </summary>
        public const int MaxTenantIdsLength = 128 * 1024;
        #endregion
        #region 实例
        /// <summary>
        /// 唯一名称
        /// </summary>
        [Required]
        [MaxLength(MaxNotificationNameLength)]
        public virtual string NotificationName { get; set; }
        /// <summary>
        /// JSON格式数据
        /// </summary>
        [MaxLength(MaxDataLength)]
        public virtual string Data { get; set; }
        [MaxLength(MaxDataTypeNameLength)]
        public virtual string DataTypeName { get; set; }
        /// <summary>
        /// 获取/设置实体类型名称
        /// </summary>
        [MaxLength(MaxEntityTypeNameLength)]
        public virtual string EntityTypeName { get; set; }
        [MaxLength(MaxEntityTypeAssemblyQualifiedNameLength)]
        public virtual string EntityTypeAssemblyQualifiedName { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        [MaxLength(MaxEntityIdLength)]
        public virtual string EntityId { get; set; }
        /// <summary>
        /// 通知等级
        /// </summary>
        public virtual NotificationSeverity Severity { get; set; }
        /// <summary>
        /// 用户iD
        /// 如果为空，则通知所有订阅的用户
        /// </summary>
        [MaxLength(MaxUserIdsLength)]
        public virtual string UserIds { get; set; }
        /// <summary>
        /// 包含的用户.
        /// 可以将此设置为在向订阅用户发布通知时排除某些用户。        
        /// </summary>
        [MaxLength(MaxUserIdsLength)]
        public virtual string ExcludedUserIds { get; set; }
        /// <summary>
        /// 租户集合
        /// 用于向特定租户的订阅用户发送通知。
        /// 仅当UserIds为null时，此选项才有效
        /// 如果是“0”，则表示所有租户。
        /// </summary>
        [MaxLength(MaxTenantIdsLength)]
        public virtual string TenantIds { get; set; }
        #endregion
        #region 构造函数
        public NotificationInfo()
        {

        }
        public NotificationInfo(Guid id)
        {
            Id = id;
            Severity = NotificationSeverity.Info;
        }
        #endregion
    }
}
