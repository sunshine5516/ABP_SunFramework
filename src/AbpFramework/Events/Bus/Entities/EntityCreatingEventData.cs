using System;
namespace AbpFramework.Events.Bus.Entities
{
    /// <summary>
    /// 此类事件用于在创建实体之前通知。
    /// </summary>
    [Serializable]
    public class EntityCreatingEventData<TEntity> : EntityChangingEventData<TEntity>
    {
        public EntityCreatingEventData(TEntity entity)
            : base(entity)
        {

        }
    }
}
