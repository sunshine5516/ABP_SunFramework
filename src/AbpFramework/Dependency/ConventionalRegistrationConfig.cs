using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Dependency
{
    /// <summary>
    /// 类用于以传统方式注册类时传递配置
    /// </summary>
    public class ConventionalRegistrationConfig
    {
        /// <summary>
        /// 是否注册所有的<see cref ="IInterceptor"/>实现。
        /// 默认: true. 
        /// </summary>
        public bool InstallInstallers { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ConventionalRegistrationConfig()
        {
            InstallInstallers = true;
        }
    }
}
