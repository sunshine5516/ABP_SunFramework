using AbpFramework;
using AbpFramework.Configuration.Startup;
using AbpFramework.Dependency;
using AbpFramework.MultiTenancy;
using AbpFramework.Runtime;
using AbpFramework.Runtime.Session;
using System;
namespace Abp.TestBase.TestBase.Runtime
{
    public class TestAbpSession : IAbpSession, ISingletonDependency
    {
        #region 声明实例
        private readonly IMultiTenancyConfig _multiTenancy;
        private readonly IAmbientScopeProvider<SessionOverride> _sessionOverrideScopeProvider;
        //private readonly ITenantResolver _tenantResolver;
        private int? _tenantId;
        private long? _userId;
        public virtual long? UserId
        {
            get
            {
                if (_sessionOverrideScopeProvider.GetValue(AbpSessionBase.SessionOverrideContextKey) != null)
                {
                    return _sessionOverrideScopeProvider.GetValue(AbpSessionBase.SessionOverrideContextKey).UserId;
                }
                return _userId;
            }
            set
            {
                _userId = value;
            }
        }

        public virtual int? TenantId
        {
            get
            {
                if (!_multiTenancy.IsEnabled)
                {
                    return 1;
                }
                if (_sessionOverrideScopeProvider.GetValue(AbpSessionBase.SessionOverrideContextKey) != null)
                {
                    return _sessionOverrideScopeProvider.GetValue(AbpSessionBase.SessionOverrideContextKey).TenantId;
                }
                //var resolvedValue = _tenantResolver.ResolveTenantId();
                //if (resolvedValue != null)
                //{
                //    return resolvedValue;
                //}
                return _tenantId;
            }
            set
            {
                if (!_multiTenancy.IsEnabled && value != 1 && value != null)
                {
                    throw new AbpException("Can not set TenantId since multi-tenancy" +
                        " is not enabled. Use IMultiTenancyConfig.IsEnabled to enable it.");
                }
                _tenantId = value;
            }
        }

        public virtual MultiTenancySides MultiTenancySide { get { return GetCurrentMultiTenancySide(); } }

        public virtual long? ImpersonatorUserId { get; set; }

        public virtual int? ImpersonatorTenantId { get; set; }
        #endregion
        #region 构造函数
        public TestAbpSession(IMultiTenancyConfig multiTenancy,
            IAmbientScopeProvider<SessionOverride> sessionOverrideScopeProvider)//,
            //ITenantResolver tenantResolver)
        {
            _multiTenancy = multiTenancy;
            _sessionOverrideScopeProvider = sessionOverrideScopeProvider;
            //_tenantResolver = tenantResolver;
        }
        #endregion
        #region 方法
        protected virtual MultiTenancySides GetCurrentMultiTenancySide()
        {
            return _multiTenancy.IsEnabled && !TenantId.HasValue
                ? MultiTenancySides.Host
                : MultiTenancySides.Tenant;
        }


        public IDisposable Use(int? tenantId, long? userId)
        {
            return _sessionOverrideScopeProvider.BeginScope(AbpSessionBase.SessionOverrideContextKey, new SessionOverride(tenantId, userId));
        }
        #endregion

    }
}
