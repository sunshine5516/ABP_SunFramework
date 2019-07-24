using System.Collections.Generic;
namespace AbpFramework.Configuration
{
    /// <summary>
    /// 定义设置定义管理器。
    /// 主要完成注册到ABP中的SettingDefinition初始化
    /// </summary>
    public interface ISettingDefinitionManager
    {
        /// <summary>
        /// 获取具有给定唯一名称的<see cref ="SettingDefinition"/>对象。
        /// 如果找不到设置，则抛出异常。
        /// </summary>
        /// <param name="name">setting的唯一名称</param>
        /// <returns>The <see cref="SettingDefinition"/> object.</returns>
        SettingDefinition GetSettingDefinition(string name);
        /// <summary>
        ///获取所有设置定义的列表。
        /// </summary>
        /// <returns>All settings.</returns>
        IReadOnlyList<SettingDefinition> GetAllSettingDefinitions();

    }
}
