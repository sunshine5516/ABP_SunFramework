using AbpFramework.Domain.Entities;
using AbpFramework.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Abp.Zero.Common.Authorization.Roles
{
    [Table("AbpRoles")]
    public abstract class AbpRoleBase : FullAuditedEntity<int>, IMayHaveTenant
    {
        /// <summary>
        /// 显示名称最大值
        /// </summary>
        public const int MaxDisplayNameLength = 64;

        /// <summary>
        /// 名称长度最大值.
        /// </summary>
        public const int MaxNameLength = 32;

        /// <summary>
        /// 租户ID.
        /// </summary>
        public virtual int? TenantId { get; set; }

        /// <summary>
        /// 唯一名称.
        /// </summary>
        [Required]
        [StringLength(MaxNameLength)]
        public virtual string Name { get; set; }
        /// <summary>
        /// 显示名称.
        /// </summary>
        [Required]
        [StringLength(MaxDisplayNameLength)]
        public virtual string DisplayName { get; set; }
        /// <summary>
        /// 是否是静态角色?
        /// 静态角色无法删除，无法更改其名称.
        /// </summary>
        public virtual bool IsStatic { get; set; }
        /// <summary>
        /// 该角色是否是默认赋给新用户的?
        /// </summary>
        public virtual bool IsDefault { get; set; }
        /// <summary>
        /// 权限集合.
        /// </summary>
        [ForeignKey("RoleId")]
        public virtual ICollection<RolePermissionSetting> Permissions { get; set; }
        protected AbpRoleBase()
        {
            Name = Guid.NewGuid().ToString("N");
        }

        protected AbpRoleBase(int? tenantId, string displayName)
            : this()
        {
            TenantId = tenantId;
            DisplayName = displayName;
        }

        protected AbpRoleBase(int? tenantId, string name, string displayName)
            : this(tenantId, displayName)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"[Role {Id}, Name={Name}]";
        }
    }
}
