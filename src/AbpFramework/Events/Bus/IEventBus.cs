using AbpFramework.Events.Bus.Factories;
using AbpFramework.Events.Bus.Handlers;
using System;
using System.Threading.Tasks;

namespace AbpFramework.Events.Bus
{
    /// <summary>
    /// 定义了一系列注册，注销和触发事件处理函数的接口
    /// </summary>
    public interface IEventBus
    {
        #region Register 注册事件
        IDisposable Register<TEventData>(Action<TEventData> action)
            where TEventData : IEventData;
        IDisposable Register<TEventData>(IEventHandler<TEventData> handler)
            where TEventData : IEventData;
        IDisposable Register<TEventData, THandler>()
            where TEventData : IEventData where THandler : IEventHandler<TEventData>, new();
        IDisposable Register(Type eventType, IEventHandler handler);
        IDisposable Register<TEventData>(IEventHandlerFactory handlerFactory)
            where TEventData : IEventData;
        IDisposable Register(Type eventType, IEventHandlerFactory handlerFactory);
        #endregion
        #region Unregister
        void Unregister<TEventData>(Action<TEventData> action)
            where TEventData : IEventData;
        void Unregister<TEventData>(IEventHandler<TEventData> handler)
            where TEventData : IEventData;
        void Unregister(Type eventType, IEventHandler handler);
        void Unregister<TEventData>(IEventHandlerFactory factory)
            where TEventData : IEventData;
        void Unregister(Type eventType, IEventHandlerFactory factory);
        void UnregisterAll<TEventData>() where TEventData : IEventData;
        void UnregisterAll(Type eventType);
        #endregion
        #region Trigger触发事件
        void Trigger<TEventData>(TEventData eventData)
            where TEventData : IEventData;
        void Trigger<TEventData>(object eventSource, TEventData eventData)
            where TEventData : IEventData;
        void Trigger(Type eventType, IEventData eventData);
        void Trigger(Type eventType, object eventSource, IEventData eventData);
        Task TriggerAsync<TEventData>(TEventData eventData) where TEventData : IEventData;
        Task TriggerAsync<TEventData>(object eventSource, TEventData eventData)
            where TEventData : IEventData;
        Task TriggerAsync(Type eventType, IEventData eventData);
        Task TriggerAsync(Type eventType, object eventSource, IEventData eventData);

        #endregion
    }
}
