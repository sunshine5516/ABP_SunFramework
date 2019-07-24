using AbpFramework.Events.Bus.Handlers;
namespace AbpFramework.Events.Bus.Factories
{
    /// <summary>
    /// 为负责创建/获取和释放事件处理程序的工厂定义接口。
    /// </summary>
    public interface IEventHandlerFactory
    {
        IEventHandler GetHandler();
        void ReleaseHandler(IEventHandler handler);
    }
}
