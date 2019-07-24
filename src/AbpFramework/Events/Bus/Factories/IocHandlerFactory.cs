using System;
using AbpFramework.Dependency;
using AbpFramework.Events.Bus.Handlers;
namespace AbpFramework.Events.Bus.Factories
{
    public class IocHandlerFactory : IEventHandlerFactory
    {
        public Type HandlerType { get; private set; }
        private readonly IIocResolver _iocResolver;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iocResolver"></param>
        /// <param name="handlerType">Type of the handler</param>
        public IocHandlerFactory(IIocResolver iocResolver, Type handlerType)
        {
            _iocResolver = iocResolver;
            HandlerType = handlerType;
        }
        public IEventHandler GetHandler()
        {
            return (IEventHandler)_iocResolver.Resolve(HandlerType);
        }

        public void ReleaseHandler(IEventHandler handler)
        {
            _iocResolver.Release(handler);
        }
    }
}
