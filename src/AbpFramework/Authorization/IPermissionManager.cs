using AbpFramework.MultiTenancy;
using System.Collections.Generic;
namespace AbpFramework.Authorization
{
    /// <summary>
    /// 权限管理接口
    /// </summary>
    public interface IPermissionManager
    {
        Permission GetPermission(string name);
        Permission GetPermissionOrNull(string name);
        /// <summary>
        /// 获取所有权限.
        /// </summary>
        /// <param name="tenancyFilter">Can be passed false to disable tenancy filter.</param>
        IReadOnlyList<Permission> GetAllPermissions(bool tenancyFilter = true);

        /// <summary>
        /// Gets all permissions.
        /// </summary>
        /// <param name="multiTenancySides">Multi-tenancy side to filter</param>
        IReadOnlyList<Permission> GetAllPermissions(MultiTenancySides multiTenancySides);
    }
}
