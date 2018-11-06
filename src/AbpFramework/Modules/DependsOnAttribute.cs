using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Modules
{
    /// <summary>
    /// 用于定义ABP模块对其他模块的依赖关系。
    /// It should be used for a class derived from <see cref="AbpModule"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DependsOnAttribute:Attribute
    {
        /// <summary>
        /// 依赖模块的类型
        /// </summary>
        public Type[] DependedModuleTypes { get; private set; }
        /// <summary>
        /// 用于定义ABP模块对其他模块的依赖关系。
        /// </summary>
        /// <param name="dependedModuleTypes"></param>
        public DependsOnAttribute(params Type[] dependedModuleTypes)
        {
            DependedModuleTypes = dependedModuleTypes;
        }
    }
}
