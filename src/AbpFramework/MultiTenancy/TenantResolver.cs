using AbpFramework.Configuration.Startup;
using AbpFramework.Dependency;
using Castle.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.MultiTenancy
{
    public class TenantResolver : ITenantResolver, ITransientDependency
    {
        #region 声明实例
        private const string AmbientScopeContextKey = "Abp.MultiTenancy.TenantResolver.Resolving";
        public ILogger Logger { get; set; }
        private readonly IMultiTenancyConfig _multiTenancy;
        private readonly IIocManager _iocManager;
        //private readonly ITenantStore _tenantStore;
        #endregion
        #region 构造函数

        #endregion
        #region 方法

        #endregion
        public int? ResolveTenantId()
        {
            return 1;
        }
    }
}
