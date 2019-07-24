using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using AbpFramework.Collections.Extensions;
using AbpFramework.Configuration.Startup;
using AbpFramework.Dependency;
using AbpFramework.MultiTenancy;
using AbpFramework.Runtime.Session;
namespace AbpFramework.Authorization
{
    /// <summary>
    /// 权限管理实现
    /// </summary>
    internal class PermissionManager : PermissionDefinitionContextBase,
        IPermissionManager, ISingletonDependency
    {
        #region 声明实例
        public IAbpSession AbpSession { get; set; }
        private readonly IIocManager _iocManager;
        private readonly IAuthorizationConfiguration _authorizationConfiguration;
        #endregion
        #region 构造函数
        public PermissionManager(IIocManager iocManager,
            IAuthorizationConfiguration authorizationConfiguration)
        {
            this._iocManager = iocManager;
            this._authorizationConfiguration = authorizationConfiguration;
        }
        #endregion
        #region 方法
        public void Initialize()
        {
            foreach (var providerType in _authorizationConfiguration.Providers)
            {
                using (var provider = _iocManager.ResolveAsDisposable<AuthorizationProvider>(providerType))
                {
                    provider.Object.SetPermissions(this);
                }
            }
            Permissions.AddAllPermissions();
        }
        public IReadOnlyList<Permission> GetAllPermissions(bool tenancyFilter = true)
        {
            return Permissions.Values
                    .WhereIf(tenancyFilter, p => p.MultiTenancySides.HasFlag(AbpSession.MultiTenancySide))
                    .Where(p =>
                        AbpSession.MultiTenancySide == MultiTenancySides.Host
                    ).ToImmutableList();
        }

        public IReadOnlyList<Permission> GetAllPermissions(MultiTenancySides multiTenancySides)
        {
            //using (var featureDependencyContext = _iocManager.ResolveAsDisposable<FeatureDependencyContext>())
            //{
                //var featureDependencyContextObject = featureDependencyContext.Object;
                return Permissions.Values
                    .Where(p => p.MultiTenancySides.HasFlag(multiTenancySides))
                    .Where(p =>
                        AbpSession.MultiTenancySide == MultiTenancySides.Host ||
                        (p.MultiTenancySides.HasFlag(MultiTenancySides.Host) &&
                         multiTenancySides.HasFlag(MultiTenancySides.Host))
                    ).ToImmutableList();
            //}
        }

        public Permission GetPermission(string name)
        {
            var permission= Permissions.GetOrDefault(name);
            if(permission==null)
            {
                throw new AbpException("There is no permission with name: " + name);
            }
            return permission;
        }
        #endregion

    }
}
