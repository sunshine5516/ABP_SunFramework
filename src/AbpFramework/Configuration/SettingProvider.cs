using AbpFramework.Dependency;
using System.Collections.Generic;
namespace AbpFramework.Configuration
{
    /// <summary>
    /// 定义模块/应用程序的设置
    /// </summary>
    public abstract class SettingProvider: ITransientDependency
    {
        public abstract IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context);
    }
}
