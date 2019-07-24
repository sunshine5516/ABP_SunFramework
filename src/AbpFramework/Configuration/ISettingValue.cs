namespace AbpFramework.Configuration
{
    /// <summary>
    /// setting的值
    /// </summary>
    public interface ISettingValue
    {
        /// <summary>
        /// 唯一名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// setting的值
        /// </summary>
        string Value { get; }
    }
}
