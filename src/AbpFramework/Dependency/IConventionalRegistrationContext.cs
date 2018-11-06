using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Dependency
{
    public interface IConventionalRegistrationContext
    {
        /// <summary>
        /// 要注册的程序集
        /// </summary>
        Assembly Assembly { get; }
        IIocManager IocManager { get; }
        /// <summary>
        /// 注册的配置
        /// </summary>
        ConventionalRegistrationConfig Config { get; }
    }
}
