using AbpFramework.Events.Bus.Handlers;
using System;
namespace AbpFramework.Events.Bus.Factories.Internals
{
    /// <summary>
    /// 该工厂每次都会创建一个新的EventHandler的实例。
    /// </summary>
    /// <typeparam name="THandler"></typeparam>
    internal class TransientEventHandlerFactory<THandler> : IEventHandlerFactory
        where THandler : IEventHandler, new()
    {
        /// <summary>
        /// 返回一个处理器对象实例
        /// </summary>
        /// <returns></returns>
        public IEventHandler GetHandler()
        {
            return new THandler();
        }

        public void ReleaseHandler(IEventHandler handler)
        {
            if(handler is IDisposable)
            {
                (handler as IDisposable).Dispose();
            }
        }
    }
}
