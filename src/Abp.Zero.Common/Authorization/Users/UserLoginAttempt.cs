using Abp.MultiTenancy;
using AbpFramework.Domain.Entities;
using AbpFramework.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Abp.Zero.Common.Authorization.Users
{
    /// <summary>
    /// 用于保存用户的登录尝试。
    /// </summary>
    [Table("AbpUserLoginAttempts")]
    public class UserLoginAttempt : Entity<long>, IHasCreationTime, IMayHaveTenant
    {
        /// <summary>
        /// <see cref="TenancyName"/>最大长度.
        /// </summary>
        public const int MaxTenancyNameLength = AbpTenantBase.MaxTenancyNameLength;

        /// <summary>
        /// <see cref="TenancyName"/>最大长度.
        /// </summary>
        public const int MaxUserNameOrEmailAddressLength = 255;

        /// <summary>
        /// <see cref="ClientIpAddress"/>最大长度.
        /// </summary>
        public const int MaxClientIpAddressLength = 64;

        /// <summary>
        /// <see cref="ClientName"/>最大长度.
        /// </summary>
        public const int MaxClientNameLength = 128;

        /// <summary>
        /// <see cref="BrowserInfo"/>最大长度.
        /// </summary>
        public const int MaxBrowserInfoLength = 256;

        /// <summary>
        /// Tenant's Id, if <see cref="TenancyName"/> was a valid tenant name.
        /// </summary>
        public virtual int? TenantId { get; set; }

        /// <summary>
        /// 租户名称.
        /// </summary>
        [MaxLength(MaxTenancyNameLength)]
        public virtual string TenancyName { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public virtual long? UserId { get; set; }

        /// <summary>
        /// 用户名或电子邮件
        /// </summary>
        [MaxLength(MaxUserNameOrEmailAddressLength)]
        public virtual string UserNameOrEmailAddress { get; set; }

        /// <summary>
        /// IP address.
        /// </summary>
        [MaxLength(MaxClientIpAddressLength)]
        public virtual string ClientIpAddress { get; set; }

        /// <summary>
        /// 客户端名称.
        /// </summary>
        [MaxLength(MaxClientNameLength)]
        public virtual string ClientName { get; set; }

        /// <summary>
        /// 浏览器信息.
        /// </summary>
        [MaxLength(MaxBrowserInfoLength)]
        public virtual string BrowserInfo { get; set; }

        /// <summary>
        /// 登录结果
        /// </summary>
        public virtual AbpLoginResultType Result { get; set; }

        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserLoginAttempt()
        {
            CreationTime = DateTime.Now;
        }
    }
}
