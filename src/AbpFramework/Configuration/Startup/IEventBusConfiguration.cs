namespace AbpFramework.Configuration.Startup
{
    /// <summary>
    /// <see cref="IEventBus"/>.配置
    /// </summary>
    public interface IEventBusConfiguration
    {
        /// <summary>
        /// True, 使用<see cref="EventBus.Default"/>.
        /// False, 使用 <see cref="IIocManager"/>.
        /// 默认为true
        /// </summary>
        bool UseDefaultEventBus { get; set; }
    }
}
