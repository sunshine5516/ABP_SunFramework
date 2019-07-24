using AbpFramework.Configuration.Startup;
using AbpFramework.Runtime.Remoting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Runtime.Session
{
    public class NullAbpSession : AbpSessionBase
    {
        #region 声明实例

        #endregion
        #region 构造函数
        public static NullAbpSession Instance { get; } = new NullAbpSession();

        public override long? UserId => null;

        public override int? TenantId => null;

        public override long? ImpersonatorUserId => null;

        public override int? ImpersonatorTenantId => null;

        private NullAbpSession()
            : base(
                  new MultiTenancyConfig(),
                  new DataContextAmbientScopeProvider<SessionOverride>(new AsyncLocalAmbientDataContext())
            )
        {

        }
        #endregion
    }
}
