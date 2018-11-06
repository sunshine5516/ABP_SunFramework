using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Modules
{
    /// <summary>
    /// 封装AbpModule的基本信息，主要包括Assembly（所属程序集）、
    /// Type（类型）、Dependencies（依赖的模块）、IsLoadedAsPlugIn（是否插件模块）
    /// </summary>
    public class AbpModuleInfo
    {
        /// <summary>
        /// 包含模块定义的程序集.
        /// </summary>
        public Assembly Assembly { get; }
        /// <summary>
        /// 模块的类型
        /// </summary>
        public Type Type { get; set; }
        /// <summary>
        /// 模块的实例   
        /// </summary>
        public AbpModule Instance { get; set; }
        /// <summary>
        /// 是否作为插件加载.
        /// </summary>
        public bool IsLoadedAsPlugIn { get; }

        /// <summary>
        /// 依赖模块.
        /// </summary>
        public List<AbpModuleInfo> Dependencies { get; }
        /// <summary>
        /// 创建一个新的AbpModuleInfo对象。
        /// </summary>
        public AbpModuleInfo([NotNull] Type type, [NotNull] AbpModule instance, bool isLoadedAsPlugIn)
        {
            //Check.NotNull(type, nameof(type));
            //Check.NotNull(instance, nameof(instance));

            Type = type;
            Instance = instance;
            IsLoadedAsPlugIn = isLoadedAsPlugIn;
            Assembly = Type.GetTypeInfo().Assembly;

            Dependencies = new List<AbpModuleInfo>();
        }

        public override string ToString()
        {
            return Type.AssemblyQualifiedName ??
                   Type.FullName;
        }
    }
}
