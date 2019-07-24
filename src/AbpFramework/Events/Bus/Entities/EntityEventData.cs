using System;
namespace AbpFramework.Events.Bus.Entities
{
    [Serializable]
    public class EntityEventData<TEntity> : EventData, IEventDataWithInheritableGenericArgument
    {
        public TEntity Entity { get; private set; }
        public EntityEventData(TEntity entity)
        {
            Entity = entity;
        }
        public object[] GetConstructorArgs()
        {
            return new object[] { Entity };
        }
    }
}
