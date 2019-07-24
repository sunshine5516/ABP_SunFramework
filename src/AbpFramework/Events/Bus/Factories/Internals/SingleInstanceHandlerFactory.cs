using AbpFramework.Events.Bus.Handlers;
namespace AbpFramework.Events.Bus.Factories.Internals
{
    /// <summary>
    /// 用于创建单例EventHandler的工厂
    /// </summary>
    internal class SingleInstanceHandlerFactory : IEventHandlerFactory
    {
        public IEventHandler HandlerInstance { get; private set; }
        public SingleInstanceHandlerFactory(IEventHandler handler)
        {
            HandlerInstance = handler;
        }
        public IEventHandler GetHandler()
        {
            return HandlerInstance;
        }

        public void ReleaseHandler(IEventHandler handler)
        {
            
        }
    }
}
