using AbpFramework.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework
{
    public class AbpBootstrapperOptions
    {
        /// <summary>
        /// 禁用ABP添加的所有拦截器
        /// </summary>
        public bool DisableAllInterceptors { get; set; }
        public IIocManager IocManager { get; set; }
        public AbpBootstrapperOptions()
        {
            IocManager = Dependency.IocManager.Instance;
        }
    }
}
