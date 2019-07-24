using AbpFramework.Configuration.Startup;
using AbpFramework.Dependency;
using AbpFramework.Events.Bus.Factories;
using AbpFramework.Events.Bus.Handlers;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System.Reflection;
namespace AbpFramework.Events.Bus
{
    /// <summary>
    /// 实现了IWindsorInstaller接口，加载事件总线系统，注册所有的处理器
    /// 第一，Register IEventBus和EventBus到依赖注入框架中，并且resolve IEventBus以得到EventBus的实例。
    /// 第二，将所有实现了IEventHandler<in TEventData>的类都会添加到Eventbus的_handlerFactories这个集合中
    /// </summary>
    internal class EventBusInstaller : IWindsorInstaller
    {
        #region 声明实例
        private readonly IIocResolver _iocResolver;
        private readonly IEventBusConfiguration _eventBusConfiguration;
        private IEventBus _eventBus;
        #endregion
        #region 构造函数
        public EventBusInstaller(IIocResolver iocResolver)
        {
            _iocResolver = iocResolver;
            _eventBusConfiguration = iocResolver.Resolve<IEventBusConfiguration>();
        }
        #endregion
        #region 方法
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            if(_eventBusConfiguration.UseDefaultEventBus)
            {
                container.Register(
                    Component.For<IEventBus>().UsingFactoryMethod(
                        () => EventBus.Default).LifestyleSingleton());
            }
            else
            {
                container.Register(
                    Component.For<IEventBus>().ImplementedBy<EventBus>()
                    .LifestyleSingleton());
                    
            }
            _eventBus = container.Resolve<IEventBus>();
            container.Kernel.ComponentRegistered += Kernel_ComponentRegistered;
        }

        private void Kernel_ComponentRegistered(string key, IHandler handler)
        {
            /* This code checks if registering component implements any IEventHandler<TEventData> interface, if yes,
             * gets all event handler interfaces and registers type to Event Bus for each handling event.
             */
            if (!typeof(IEventHandler).GetTypeInfo().IsAssignableFrom(handler.ComponentModel.Implementation))
            {
                return;
            }
            var interfaces = handler.ComponentModel.Implementation.GetTypeInfo().GetInterfaces();
            foreach(var @interface in interfaces)
            {
                if (!typeof(IEventHandler).GetTypeInfo().IsAssignableFrom(@interface))
                {
                    continue;
                }
                var genericArgs = @interface.GetGenericArguments();
                if (genericArgs.Length == 1)
                {
                    _eventBus.Register(genericArgs[0], new IocHandlerFactory
                        (_iocResolver, handler.ComponentModel.Implementation));
                }
            }
        }
        #endregion
    }
}
