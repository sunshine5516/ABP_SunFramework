using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.Zero.Common.Authorization.Roles
{
    /// <summary>
    /// 用于为角色执行权限数据库操作
    /// </summary>
    /// <typeparam name="TRole"></typeparam>
    public interface IRolePermissionStore<in TRole>
        where TRole : AbpRoleBase
    {
        /// <summary>
        /// 给角色添加权限
        /// </summary>
        /// <param name="role">角色</param>
        /// <param name="permissionGrantInfo"></param>
        /// <returns></returns>
        Task AddPermissionAsync(TRole role, PermissionGrantInfo permissionGrantInfo);
        /// <summary>
        /// 删除角色权限
        /// </summary>
        /// <param name="role">角色</param>
        /// <param name="permissionGrant"></param>
        /// <returns></returns>
        Task RemovePermissionAsync(TRole role, PermissionGrantInfo permissionGrant);
        /// <summary>
        ///获取角色权限
        /// </summary>
        /// <param name="role">角色</param>
        /// <returns>权限集合</returns>
        Task<IList<PermissionGrantInfo>> GetPermissionsAsync(TRole role);
        /// <summary>
        /// 根据角色ID获取权限集合.
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>权限集合</returns>
        Task<IList<PermissionGrantInfo>> GetPermissionsAsync(int roleId);

        /// <summary>
        /// 检查角色是否有权限.
        /// </summary>
        /// <param name="roleId">Role id</param>
        /// <param name="permissionGrant">Permission grant setting info</param>
        /// <returns></returns>
        Task<bool> HasPermissionAsync(int roleId, PermissionGrantInfo permissionGrant);

        /// <summary>
        /// 删除角色权限.
        /// </summary>
        /// <param name="role">Role</param>
        Task RemoveAllPermissionSettingsAsync(TRole role);
    }
}
