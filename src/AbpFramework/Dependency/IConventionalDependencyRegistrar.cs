using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Dependency
{
    /// <summary>
    /// 依赖注入接口
    /// </summary>
    public interface IConventionalDependencyRegistrar
    {
        /// <summary>
        /// 注册程序集
        /// </summary>
        /// <param name="context"></param>
        void RegisterAssembly(IConventionalRegistrationContext context);
    }
}
