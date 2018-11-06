using System;
namespace AbpFramework.Auditing
{
    /// <summary>
    /// 审计信息实体
    /// </summary>
    public class AuditInfo
    {
        /// <summary>
        /// 租户Id
        /// </summary>
        public int? TenantId { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public long? UserId { get; set; }
        public long? ImpersonatorUserId { get; set; }
        public int? ImpersonatorTenantId { get; set; }
        /// <summary>
        /// 服务(类/接口)名称.
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// 执行方法名称
        /// </summary>
        public string MethodName { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        public string Parameters { get; set; }
        /// <summary>
        /// 执行时间.
        /// </summary>
        public DateTime ExecutionTime { get; set; }
        /// <summary>
        /// 方法调用的总持续时间.
        /// </summary>
        public int ExecutionDuration { get; set; }

        /// <summary>
        /// 客户端IP.
        /// </summary>
        public string ClientIpAddress { get; set; }

        /// <summary>
        /// 客户端名称.
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// 浏览器信息
        /// </summary>
        public string BrowserInfo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CustomData { get; set; }

        /// <summary>
        /// 异常信息.
        /// </summary>
        public Exception Exception { get; set; }
        public override string ToString()
        {
            var loggedUserId = UserId.HasValue
                                   ? "user " + UserId.Value
                                   : "an anonymous user";

            var exceptionOrSuccessMessage = Exception != null
                ? "exception: " + Exception.Message
                : "succeed";

            return $"AUDIT LOG: {ServiceName}.{MethodName} is executed by " +
                $"{loggedUserId} in {ExecutionDuration} ms from {ClientIpAddress} " +
                $"IP address with {exceptionOrSuccessMessage}.";
        }

    }
}
