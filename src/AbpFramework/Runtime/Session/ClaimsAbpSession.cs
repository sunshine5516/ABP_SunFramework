using AbpFramework.Configuration.Startup;
using AbpFramework.Dependency;
using AbpFramework.MultiTenancy;
using AbpFramework.Runtime.Security;
using System.Linq;

namespace AbpFramework.Runtime.Session
{
    public class ClaimsAbpSession:AbpSessionBase, ISingletonDependency
    {
        #region 声明实例
        protected IPrincipalAccessor PrincipalAccessor { get; }
        protected ITenantResolver TenantResolver { get; }
        #endregion
        #region 构造函数
        public ClaimsAbpSession(
        IPrincipalAccessor principalAccessor,
        IMultiTenancyConfig multiTenancy,
        //ITenantResolver tenantResolver,
        IAmbientScopeProvider<SessionOverride> sessionOverrideScopeProvider)
        : base(
              multiTenancy,
              sessionOverrideScopeProvider)
        {
            //TenantResolver = tenantResolver;
            PrincipalAccessor = principalAccessor;
        }
        #endregion
        #region 方法
        public override long? UserId
        {
            get

            {
                if(OverridedValue!=null)
                {
                    return OverridedValue.UserId;
                }
                var t = PrincipalAccessor.Principal?.Claims;
                var userIdClaim=PrincipalAccessor.Principal?.Claims
                    .FirstOrDefault(c => c.Type == AbpClaimTypes.UserId);

                if (string.IsNullOrEmpty(userIdClaim?.Value))
                {
                    return null;
                }
                long userId;
                if(!long.TryParse(userIdClaim.Value,out userId))
                {
                    return null;
                }
                return userId;
            }
        }

        public override int? TenantId
        {
            //get
            //{
            //    if (!MultiTenancy.IsEnabled)
            //    {
            //        return MultiTenancyConsts.DefaultTenantId;
            //    }

            //    if (OverridedValue != null)
            //    {
            //        return OverridedValue.TenantId;
            //    }

            //}
            get
            {
                return null;
            }
            
        }

        public override long? ImpersonatorUserId
        {
            get
            {
                return 1;
            }
        }

        public override int? ImpersonatorTenantId
        {
            get
            {
                return 1;
            }
        }
        #endregion
    }
}
