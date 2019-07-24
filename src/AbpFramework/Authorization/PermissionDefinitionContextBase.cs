using AbpFramework.Collections.Extensions;
using AbpFramework.MultiTenancy;
namespace AbpFramework.Authorization
{
    internal abstract class PermissionDefinitionContextBase : IPermissionDefinitionContext
    {
        #region 声明实例
        protected readonly PermissionDictionary Permissions;
        #endregion
        #region 构造函数
        protected PermissionDefinitionContextBase()
        {
            Permissions = new PermissionDictionary();
        }
        #endregion
        #region 方法
        public Permission CreatePermission(string name, string displayName = null, 
            string description = null,
            MultiTenancySides multiTenancySides = MultiTenancySides.Tenant | MultiTenancySides.Host)
        {
            if (Permissions.ContainsKey(name))
            {
                throw new AbpException("There is already a permission with name: " + name);
            }
            var permission = new Permission(name, displayName, description, multiTenancySides);
            Permissions[permission.Name] = permission;
            return permission;
        }

        public Permission GetPermissionOrNull(string name)
        {
            return Permissions.GetOrDefault(name);
        }
        #endregion

    }
}
