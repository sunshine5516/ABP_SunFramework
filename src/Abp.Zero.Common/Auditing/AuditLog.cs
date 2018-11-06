using AbpFramework.Auditing;
using AbpFramework.Domain.Entities;
using AbpFramework.Extensions;
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Abp.Zero.Common.Auditing
{
    [Table("AbpAuditLogs")]
    public class AuditLog : Entity<long>, IMayHaveTenant
    {
        public static int MaxServiceNameLength = 256;
        public static int MaxMethodNameLength = 256;
        public static int MaxParametersLength = 1024;
        public static int MaxClientIpAddressLength = 64;
        public static int MaxClientNameLength = 128;
        public static int MaxBrowserInfoLength = 256;
        public static int MaxExceptionLength = 2000;
        public static int MaxCustomDataLength = 2000;
        /// <summary>
        /// 租户ID
        /// </summary>
        public virtual int? TenantId { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public virtual long? UserId { get; set; }
        public virtual string ServiceName { get; set; }
        public virtual string MethodName { get; set; }
        public virtual string Parameters { get; set; }
        public virtual DateTime ExecutionTime { get; set; }
        public virtual int ExecutionDuration { get; set; }
        public virtual string ClientIpAddress { get; set; }
        public virtual string ClientName { get; set; }
        public virtual string BrowserInfo { get; set; }
        public virtual string Exception { get; set; }
        public virtual long? ImpersonatorUserId { get; set; }
        public virtual int? ImpersonatorTenantId { get; set; }
        public virtual string CustomData { get; set; }
        /// <summary>
        /// 构建<see cref="AuditInfo"/>实例
        /// </summary>
        /// <param name="auditInfo"></param>
        /// <returns></returns>
        public static AuditLog CreateFromAuditInfo(AuditInfo auditInfo)
        {
            var exceptionMessage = auditInfo.Exception != null ? auditInfo.Exception.ToString() : null;
            return new AuditLog
            {
                TenantId = auditInfo.TenantId,
                UserId = auditInfo.UserId,
                ServiceName = auditInfo.ServiceName.TruncateWithPostfix(MaxServiceNameLength),
                MethodName = auditInfo.MethodName.TruncateWithPostfix(MaxMethodNameLength),
                Parameters = auditInfo.Parameters.TruncateWithPostfix(MaxParametersLength),
                ExecutionTime = auditInfo.ExecutionTime,
                ExecutionDuration = auditInfo.ExecutionDuration,
                ClientIpAddress = auditInfo.ClientIpAddress.TruncateWithPostfix(MaxClientIpAddressLength),
                ClientName = auditInfo.ClientName.TruncateWithPostfix(MaxClientNameLength),
                BrowserInfo = auditInfo.BrowserInfo.TruncateWithPostfix(MaxBrowserInfoLength),
                Exception = exceptionMessage.TruncateWithPostfix(MaxExceptionLength),
                ImpersonatorUserId = auditInfo.ImpersonatorUserId,
                ImpersonatorTenantId = auditInfo.ImpersonatorTenantId,
                CustomData = auditInfo.CustomData.TruncateWithPostfix(MaxCustomDataLength)
            };
        }
    }
}
