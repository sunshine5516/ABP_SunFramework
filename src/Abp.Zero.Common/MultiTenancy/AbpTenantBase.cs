using AbpFramework.Domain.Entities;
using AbpFramework.Domain.Entities.Auditing;
using AbpFramework.MultiTenancy;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Abp.MultiTenancy
{
    /// <summary>
    /// 租户基类
    /// </summary>
    [Table("AbpTenants")]
    [MultiTenancySide(MultiTenancySides.Host)]
    public abstract class AbpTenantBase : FullAuditedEntity<int>, IPassivable
    {
        /// <summary>
        /// 名称最大长度.
        /// </summary>
        public const int MaxTenancyNameLength = 64;

        /// <summary>
        /// <see cref="ConnectionString"/>最大长度.
        /// </summary>
        public const int MaxConnectionStringLength = 1024;

        /// <summary>
        /// 默认名称
        /// </summary>
        public const string DefaultTenantName = "Default";

        /// <summary>
        /// "^[a-zA-Z][a-zA-Z0-9_-]{1,}$".
        /// </summary>
        public const string TenancyNameRegex = "^[a-zA-Z][a-zA-Z0-9_-]{1,}$";

        /// <summary>
        /// <see cref="Name"/>最大长度.
        /// </summary>
        public const int MaxNameLength = 128;
        /// <summary>
        /// 租户名称，名称唯一.
        /// 可以在Web应用程序中用作子域名。
        /// </summary>
        [Required]
        [StringLength(MaxTenancyNameLength)]
        public virtual string TenancyName { get; set; }
        /// <summary>
        /// 显示名称.
        /// </summary>
        [Required]
        [StringLength(MaxNameLength)]
        public virtual string Name { get; set; }
        /// <summary>
        /// 数据库连接字符串
        /// 如果此租户存储在主机数据库中，则可以为null。
        /// Use <see cref="SimpleStringCipher"/> to encrypt/decrypt this.
        /// </summary>
        [StringLength(MaxConnectionStringLength)]
        public virtual string ConnectionString { get; set; }
        /// <summary>
        /// 租户是否可用
        /// 如果该租户未未激活状态，则这种租户类型的用户不可使用该应用；
        /// </summary>
        public virtual bool IsActive { get; set; }
    }
}
