using AbpFramework.Domain.Entities.Auditing;
using AbpFramework.MultiTenancy;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Abp.Zero.Common.Application.Editions
{
    /// <summary>
    /// 表示应用程序的一个版本
    /// </summary>
    [Table("AbpEditions")]
    [MultiTenancySide(MultiTenancySides.Host)]
    public class Edition : FullAuditedEntity
    {
        /// <summary>
        /// 名称最大长度
        /// </summary>
        public const int MaxNameLength = 32;

        /// <summary>
        /// 显示名称最大长度
        /// </summary>
        public const int MaxDisplayNameLength = 64;
        /// <summary>
        /// 唯一名称
        /// </summary>
        [Required]
        [StringLength(MaxNameLength)]
        public virtual string Name { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        [Required]
        [StringLength(MaxDisplayNameLength)]
        public virtual string DisplayName { get; set; }

        public Edition()
        {
            Name = Guid.NewGuid().ToString("N");
        }

        public Edition(string displayName)
            : this()
        {
            DisplayName = displayName;
        }
    }
}
