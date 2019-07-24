using System.Threading.Tasks;
namespace AbpFramework.Events.Bus.Entities
{
    /// <summary>
    /// 用于触发实体更改事件
    /// </summary>
    public interface IEntityChangeEventHelper
    {
        void TriggerEvents(EntityChangeReport changeReport);
        Task TriggerEventsAsync(EntityChangeReport changeReport);
        void TriggerEntityCreatingEvent(object entity);
        void TriggerEntityCreatedEventOnUowCompleted(object entity);
        void TriggerEntityUpdatingEvent(object entity);
        void TriggerEntityUpdatedEventOnUowCompleted(object entity);

        void TriggerEntityDeletingEvent(object entity);

        void TriggerEntityDeletedEventOnUowCompleted(object entity);
    }
}
