using System;
namespace AbpFramework.Events.Bus.Entities
{
    /// <summary>
    /// 用于在实体（<see cref ="IEntity"/>）更改（创建，更新或删除）时传递事件的数据。
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    [Serializable]
    public class EntityChangedEventData<TEntity> : EntityEventData<TEntity>
    {
        public EntityChangedEventData(TEntity entity)
            : base(entity)
        {

        }
    }
}
