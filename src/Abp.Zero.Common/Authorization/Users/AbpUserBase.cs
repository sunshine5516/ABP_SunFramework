using Abp.Zero.Common.Configuration;
using AbpFramework;
using AbpFramework.Domain.Entities;
using AbpFramework.Domain.Entities.Auditing;
using AbpFramework.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Abp.Zero.Common.Authorization.Users
{
    /// <summary>
    /// 用户基类
    /// </summary>
    [Table("AbpUsers")]
    public abstract class AbpUserBase : FullAuditedEntity<long>, IMayHaveTenant, IPassivable
    {
        /// <summary>
        /// 用户名最大长度
        /// </summary>
        public const int MaxUserNameLength = 32;

        /// <summary>
        /// 邮箱地址最大长度
        /// </summary>
        public const int MaxEmailAddressLength = 256;

        /// <summary>
        /// 名称最大长度
        /// </summary>
        public const int MaxNameLength = 32;

        /// <summary>
        /// 姓的最大长度
        /// </summary>
        public const int MaxSurnameLength = 32;

        /// <summary>
        /// Maximum length of the <see cref="AuthenticationSource"/> property.
        /// </summary>
        public const int MaxAuthenticationSourceLength = 64;

        /// <summary>
        /// 管理员名称.
        /// admin无法删除，管理员的UserName无法更改。
        /// </summary>
        public const string AdminUserName = "admin";

        /// <summary>
        /// 密码长度最大值
        /// </summary>
        public const int MaxPasswordLength = 128;

        /// <summary>
        /// Maximum length of the <see cref="Password"/> without hashed.
        /// </summary>
        public const int MaxPlainPasswordLength = 32;

        /// <summary>
        /// 电子邮件确认值得最大长度
        /// </summary>
        public const int MaxEmailConfirmationCodeLength = 328;

        /// <summary>
        /// 重置密码值的最大长度
        /// </summary>
        public const int MaxPasswordResetCodeLength = 328;

        /// <summary>
        /// Authorization source name.
        /// 如果由外部源创建，则将其设置为外部认证源名称。
        /// 默认: null.
        /// </summary>
        [MaxLength(MaxAuthenticationSourceLength)]
        public virtual string AuthenticationSource { get; set; }

        /// <summary>
        /// 用户的登录名，对于一个租户来说应该是唯一的。
        /// </summary>
        [Required]
        [StringLength(MaxUserNameLength)]
        public virtual string UserName { get; set; }

        /// <summary>
        /// 租户ID.
        /// </summary>
        public virtual int? TenantId { get; set; }

        /// <summary>
        /// 电子邮件地址，唯一的
        /// </summary>
        [Required]
        [StringLength(MaxEmailAddressLength)]
        public virtual string EmailAddress { get; set; }

        /// <summary>
        /// 用户的名称.
        /// </summary>
        [Required]
        [StringLength(MaxNameLength)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 用户的姓.
        /// </summary>
        [Required]
        [StringLength(MaxSurnameLength)]
        public virtual string Surname { get; set; }

        /// <summary>
        /// 全名
        /// </summary>
        [NotMapped]
        public virtual string FullName { get { return this.Name + " " + this.Surname; } }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [StringLength(MaxPasswordLength)]
        public virtual string Password { get; set; }

        /// <summary>
        /// 确认邮件
        /// </summary>
        [StringLength(MaxEmailConfirmationCodeLength)]
        public virtual string EmailConfirmationCode { get; set; }

        /// <summary>
        /// 重置密码值.
        /// 如果它为空则无效
        /// 这是一种用法，重置后必须设置为null。
        /// </summary>
        [StringLength(MaxPasswordResetCodeLength)]
        public virtual string PasswordResetCode { get; set; }

        /// <summary>
        /// 登出日期
        /// </summary>
        public virtual DateTime? LockoutEndDateUtc { get; set; }

        /// <summary>
        /// 访问失败计数。
        /// </summary>
        public virtual int AccessFailedCount { get; set; }

        /// <summary>
        /// 启用锁定。
        /// </summary>
        public virtual bool IsLockoutEnabled { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        /// Is the <see cref="PhoneNumber"/> confirmed.
        /// </summary>
        public virtual bool IsPhoneNumberConfirmed { get; set; }

        /// <summary>
        /// 安全戳.
        /// </summary>
        public virtual string SecurityStamp { get; set; }

        /// <summary>
        /// 是否启用了双因素身份验证。
        /// </summary>
        public virtual bool IsTwoFactorEnabled { get; set; }

        /// <summary>
        /// 用户的登录定义。
        /// </summary>
        [ForeignKey("UserId")]
        public virtual ICollection<UserLogin> Logins { get; set; }

        /// <summary>
        /// 用户角色.
        /// </summary>
        [ForeignKey("UserId")]
        public virtual ICollection<UserRole> Roles { get; set; }

        /// <summary>
        /// Claims of this user.
        /// </summary>
        [ForeignKey("UserId")]
        public virtual ICollection<UserClaim> Claims { get; set; }

        /// <summary>
        /// 用户的权限
        /// </summary>
        [ForeignKey("UserId")]
        public virtual ICollection<UserPermissionSetting> Permissions { get; set; }

        /// <summary>
        /// 设置信息.
        /// </summary>
        [ForeignKey("UserId")]
        public virtual ICollection<Setting> Settings { get; set; }

        /// <summary>
        /// 是否已确认<see cref="EmailAddress"/>.
        /// </summary>
        public virtual bool IsEmailConfirmed { get; set; }

        /// <summary>
        /// 是否激活
        /// 如果未激活，则无法使用该系统
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public virtual DateTime? LastLoginTime { get; set; }

        protected AbpUserBase()
        {
            IsActive = true;
            IsLockoutEnabled = true;
            SecurityStamp = SequentialGuidGenerator.Instance.Create().ToString();
        }

        public virtual void SetNewPasswordResetCode()
        {
            PasswordResetCode = Guid.NewGuid().ToString("N").Truncate(MaxPasswordResetCodeLength);
        }

        public virtual void SetNewEmailConfirmationCode()
        {
            EmailConfirmationCode = Guid.NewGuid().ToString("N").Truncate(MaxEmailConfirmationCodeLength);
        }

        /// <summary>
        /// Creates <see cref="UserIdentifier"/> from this User.
        /// </summary>
        /// <returns></returns>
        //public virtual UserIdentifier ToUserIdentifier()
        //{
        //    return new UserIdentifier(TenantId, Id);
        //}

        public override string ToString()
        {
            return $"[User {Id}] {UserName}";
        }
    }
}
