using System;
using AbpFramework.Configuration.Startup;
using AbpFramework.MultiTenancy;
namespace AbpFramework.Runtime.Session
{
    public abstract class AbpSessionBase : IAbpSession
    {
        public const string SessionOverrideContextKey = "Abp.Runtime.Session.Override";
        #region 实例
        public abstract long? UserId { get; }

        public abstract int? TenantId { get; }
        public IMultiTenancyConfig MultiTenancy { get; }

       

        public abstract long? ImpersonatorUserId { get; }

        public abstract int? ImpersonatorTenantId { get; }
        public MultiTenancySides MultiTenancySide
        {
            get
            {
                return MultiTenancy.IsEnabled && !TenantId.HasValue
                   ? MultiTenancySides.Host
                   : MultiTenancySides.Tenant;
            }
        }
        protected SessionOverride OverridedValue => SessionOverrideScopeProvider.GetValue(SessionOverrideContextKey);
        protected IAmbientScopeProvider<SessionOverride> SessionOverrideScopeProvider { get; }

        protected AbpSessionBase(IMultiTenancyConfig multiTenancy, IAmbientScopeProvider<SessionOverride> sessionOverrideScopeProvider)
        {
            MultiTenancy = multiTenancy;
            SessionOverrideScopeProvider = sessionOverrideScopeProvider;
        }
        #endregion
        #region 方法
        public IDisposable Use(int? tenantId, long? userId)
        {
            return SessionOverrideScopeProvider.BeginScope(SessionOverrideContextKey, new SessionOverride(tenantId, userId));
        }
        #endregion


    }
}
