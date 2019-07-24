using System;
namespace AbpFramework.Events.Bus.Entities
{
    [Serializable]
    public class EntityUpdatedEventData<TEntity> : EntityChangedEventData<TEntity>
    {
        public EntityUpdatedEventData(TEntity entity)
            :base(entity)
        {

        }
    }
}
