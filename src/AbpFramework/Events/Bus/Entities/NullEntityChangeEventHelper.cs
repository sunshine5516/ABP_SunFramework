using System.Threading.Tasks;
namespace AbpFramework.Events.Bus.Entities
{
    /// <summary>
    /// <see cref="IEntityChangeEventHelper"/>空实现
    /// </summary>
    public class NullEntityChangeEventHelper : IEntityChangeEventHelper
    {
        public static NullEntityChangeEventHelper Instance { get; } = new NullEntityChangeEventHelper();
        public void TriggerEntityCreatedEventOnUowCompleted(object entity)
        {
            
        }

        public void TriggerEntityCreatingEvent(object entity)
        {
            
        }

        public void TriggerEntityDeletedEventOnUowCompleted(object entity)
        {
            
        }

        public void TriggerEntityDeletingEvent(object entity)
        {
            
        }

        public void TriggerEntityUpdatedEventOnUowCompleted(object entity)
        {
           
        }

        public void TriggerEntityUpdatingEvent(object entity)
        {
            
        }

        public void TriggerEvents(EntityChangeReport changeReport)
        {
            
        }

        public Task TriggerEventsAsync(EntityChangeReport changeReport)
        {
            return Task.FromResult(0);
        }
    }
}
