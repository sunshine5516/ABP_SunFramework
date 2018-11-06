using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Configuration.Startup
{
    /// <summary>
    /// 模块配置类
    /// </summary>
    public interface IModuleConfigurations
    {
        /// <summary>
        /// 获取ABP配置对象
        /// </summary>
        IAbpStartupConfiguration AbpConfiguration { get; }
    }
}
