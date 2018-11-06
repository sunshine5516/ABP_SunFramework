using AbpFramework.MultiTenancy;
using System;
namespace AbpFramework.Runtime.Session
{
    /// <summary>
    /// 定义一些对应用程序有用的会话信息。
    /// </summary>
    public interface IAbpSession
    {
        /// <summary>
        /// 获取当前用户ID或者NULL
        /// 如果为登录则为空
        /// </summary>
        long? UserId { get; }
        int? TenantId { get; }
        /// <summary>
        /// Gets current multi-tenancy side.
        /// </summary>
        MultiTenancySides MultiTenancySide { get; }
        long? ImpersonatorUserId { get; }
        int? ImpersonatorTenantId { get; }
        IDisposable Use(int? tenantId, long? userId);
    }
}
