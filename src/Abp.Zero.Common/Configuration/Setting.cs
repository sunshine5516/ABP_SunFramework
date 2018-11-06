using AbpFramework.Domain.Entities;
using AbpFramework.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Abp.Zero.Common.Configuration
{
    /// <summary>
    /// 表示租户或用户的设置.
    /// </summary>
    [Table("AbpSettings")]
    public class Setting : AuditedEntity<long>, IMayHaveTenant
    {
        /// <summary>
        ///名称长度最大值
        /// </summary>
        public const int MaxNameLength = 256;

        /// <summary>
        /// <see cref="Value"/>长度最大值.
        /// </summary>
        public const int MaxValueLength = 2000;
        /// <summary>
        /// 租户ID
        /// ID未NULL如果此设置不是租户级别.
        /// </summary>
        public virtual int? TenantId { get; set; }
        /// <summary>
        /// 用户ID.
        /// ID未NULL如果此设置不是用户级别
        /// </summary>
        public virtual long? UserId { get; set; }
        /// <summary>
        /// 唯一名称
        /// </summary>
        [Required]
        [MaxLength(MaxNameLength)]
        public virtual string Name { get; set; }
        /// <summary>
        /// 值.
        /// </summary>
        [MaxLength(MaxValueLength)]
        public virtual string Value { get; set; }
        public Setting() { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tenantId">租户ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="name">唯一名称</param>
        /// <param name="value">Value</param>
        public Setting(int? tenantId, long? userId, string name, string value)
        {
            TenantId = tenantId;
            UserId = userId;
            Name = name;
            Value = value;
        }
    }
}
