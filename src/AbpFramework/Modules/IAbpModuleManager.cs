using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Modules
{
    /// <summary>
    /// 模块管理接口
    /// </summary>
    public interface IAbpModuleManager
    {
        AbpModuleInfo StartupModule { get; }
        IReadOnlyList<AbpModuleInfo> Modules { get; }
        void Initialize(Type startupModule);

        void StartModules();

        void ShutdownModules();
    }
}
