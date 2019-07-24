namespace AbpFramework.Events.Bus.Handlers
{
    /// <summary>
    /// 处理事件接口
    /// </summary>
    /// <typeparam name="TEventData">待处理事件的类型</typeparam>
    public interface IEventHandler<in TEventData>:IEventHandler
    {
        /// <summary>
        /// 事件处理
        /// </summary>
        /// <param name="eventData"></param>
        void HandleEvent(TEventData eventData);
    }
}
