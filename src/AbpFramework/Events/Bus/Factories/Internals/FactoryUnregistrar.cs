using System;
namespace AbpFramework.Events.Bus.Factories.Internals
{
    internal class FactoryUnregistrar : IDisposable
    {
        #region 声明实例
        private readonly IEventBus _eventBus;
        private readonly Type _eventType;
        private readonly IEventHandlerFactory _factory;
        #endregion
        #region 构造函数
        public FactoryUnregistrar(IEventBus eventBus, Type eventType, IEventHandlerFactory factory)
        {
            _eventBus = eventBus;
            _eventType = eventType;
            _factory = factory;
        }
        #endregion
        #region 方法
        public void Dispose()
        {
            _eventBus.Unregister(_eventType, _factory);
        }
        #endregion

    }
}
