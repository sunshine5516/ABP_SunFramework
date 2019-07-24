using AbpFramework.Dependency;
using System;
namespace AbpFramework.Events.Bus.Handlers.Internals
{
    /// <summary>
    /// 起到适配器的作用，将一个Action适配成一个事件处理器EventHandler
    /// </summary>
    /// <typeparam name="TEventData"></typeparam>
    internal class ActionEventHandler<TEventData> :
        IEventHandler<TEventData>, ITransientDependency
    {
        /// <summary>
        /// 处理事件的动作
        /// </summary>
        public Action<TEventData> Action { get; private set; }
        public ActionEventHandler(Action<TEventData> handler)
        {
            Action = handler;
        }
        public void HandleEvent(TEventData eventData)
        {
            Action(eventData);
        }
    }
}
