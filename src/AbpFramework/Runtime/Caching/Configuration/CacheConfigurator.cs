using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Runtime.Caching.Configuration
{
    public class CacheConfigurator : ICacheConfigurator
    {
        #region 声明实例
        public string CacheName { get; private set; }
        public Action<ICache> InitAction { get; }
        #endregion
        #region 构造函数
        public CacheConfigurator(Action<ICache> initAction)
        {
            InitAction = initAction;
        }
        public CacheConfigurator(string cacheName, Action<ICache> initAction)
        {
            CacheName = cacheName;
            InitAction = initAction;
        }
        #endregion
        #region 方法

        #endregion



    }
}
