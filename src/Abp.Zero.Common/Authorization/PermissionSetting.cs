using AbpFramework.Domain.Entities;
using AbpFramework.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Abp.Zero.Common.Authorization
{
    /// <summary>
    /// 用于授予/拒绝角色或用户的权限。
    /// </summary>
    [Table("AbpPermissions")]
    public abstract class PermissionSetting : CreationAuditedEntity<long>, IMayHaveTenant
    {
        /// <summary>
        /// 长度最大值.
        /// </summary>
        public const int MaxNameLength = 128;

        public virtual int? TenantId { get; set; }

        /// <summary>
        /// 唯一名称.
        /// </summary>
        [Required]
        [MaxLength(MaxNameLength)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 是否为此权限授予此角色。
        /// Default value: true.
        /// </summary>
        public virtual bool IsGranted { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        protected PermissionSetting()
        {
            IsGranted = true;
        }
    }
}
