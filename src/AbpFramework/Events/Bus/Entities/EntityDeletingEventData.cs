using System;
namespace AbpFramework.Events.Bus.Entities
{
    [Serializable]
    public class EntityDeletingEventData<TEntity> : EntityChangingEventData<TEntity>
    {
        public EntityDeletingEventData(TEntity entity)
            : base(entity)
        {

        }
    }
}
