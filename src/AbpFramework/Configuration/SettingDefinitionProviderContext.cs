namespace AbpFramework.Configuration
{
    /// <summary>
    /// 上下文类，一般用于封装方法间调用需要传递的参数。
    /// </summary>
    public class SettingDefinitionProviderContext
    {
        public ISettingDefinitionManager Manager { get; }
        internal SettingDefinitionProviderContext(ISettingDefinitionManager manager)
        {
            Manager = manager;
        }
    }
}
