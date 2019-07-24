using System;
namespace AbpFramework.Events.Bus
{
    /// <summary>
    /// 封装了EventData信息，触发event的源对象和时间
    /// </summary>
    public interface IEventData
    {
        /// <summary>
        /// 触发event的时间
        /// </summary>
        DateTime EventTime { get; set; }
        /// <summary>
        /// event的源对象
        /// </summary>
        object EventSource { get; set; }
    }
}
