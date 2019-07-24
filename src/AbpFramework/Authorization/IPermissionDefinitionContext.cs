using AbpFramework.MultiTenancy;

namespace AbpFramework.Authorization
{
    /// <summary>
    /// 在方法 <see cref="AuthorizationProvider.SetPermissions"/> 使用.
    /// </summary>
    public interface IPermissionDefinitionContext
    {
        /// <summary>
        /// 在这个组下创建一个新的权限。
        /// </summary>
        /// <param name="name">权限唯一名称</param>
        /// <param name="displayName">显示名称</param>
        /// <param name="description">描述</param>
        /// <param name="multiTenancySides">使用范围</param>
        /// <param name="featureDependency">Depended feature(s) of this permission</param>
        /// <returns></returns>
        Permission CreatePermission(
            string name,
            string displayName = null,
            string description = null,
            MultiTenancySides multiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant
            );

        /// <summary>
        /// 获取给定名称的权限，如果找不到，则返回null。
        /// </summary>
        /// <param name="name">唯一名称</param>
        /// <returns>Permission object or null</returns>
        Permission GetPermissionOrNull(string name);
    }
}
