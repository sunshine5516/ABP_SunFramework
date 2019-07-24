using System;
namespace AbpFramework.Events.Bus.Entities
{
    [Serializable]
    public class EntityUpdatingEventData<TEntity> : EntityChangingEventData<TEntity>
    {
        public EntityUpdatingEventData(TEntity entity)
            :base(entity)
        { }
    }
}
