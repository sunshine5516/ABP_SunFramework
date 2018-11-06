using AbpFramework.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Runtime.Caching.Configuration
{
    public class AbpMemoryCacheManager : CacheManagerBase
    {
        #region 构造函数
        public AbpMemoryCacheManager(IIocManager iocManager, ICachingConfiguration configuration)
           : base(iocManager, configuration)
        {
            _IocManager.RegisterIfNot<AbpMemoryCache>(DependencyLifeStyle.Transient);
        }
        #endregion
        #region 方法
        protected override ICache CreateCacheImplementation(string name)
        {
            return _IocManager.Resolve<AbpMemoryCache>(new { name });
        }
        #endregion

    }
}
