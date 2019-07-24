using System.Security.Claims;
namespace AbpFramework.Runtime.Security
{
    /// <summary>
    /// 用于获取特定于ABP的声明类型名称。
    /// </summary>
    public static class AbpClaimTypes
    {
        public static string UserId { get; set; }= ClaimTypes.NameIdentifier;
        public static string UserName { get; set; }= ClaimTypes.Name;
        public static string Role { get; set; } = ClaimTypes.Role;
        /// <summary>
        /// TenantId.
        /// 默认: http://www.aspnetboilerplate.com/identity/claims/tenantId
        /// </summary>
        public static string TenantId { get; set; } = "http://www.aspnetboilerplate.com/identity/claims/tenantId";

        /// <summary>
        /// ImpersonatorUserId.
        /// 默认: http://www.aspnetboilerplate.com/identity/claims/impersonatorUserId
        /// </summary>
        public static string ImpersonatorUserId { get; set; } = "http://www.aspnetboilerplate.com/identity/claims/impersonatorUserId";

        /// <summary>
        /// ImpersonatorTenantId
        /// 默认: http://www.aspnetboilerplate.com/identity/claims/impersonatorTenantId
        /// </summary>
        public static string ImpersonatorTenantId { get; set; } = "http://www.aspnetboilerplate.com/identity/claims/impersonatorTenantId";
    }
}
