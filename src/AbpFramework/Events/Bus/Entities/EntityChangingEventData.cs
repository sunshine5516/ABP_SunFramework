using System;
namespace AbpFramework.Events.Bus.Entities
{
    /// <summary>
    /// 用于在实体（<see cref ="IEntity"/>）被更改（创建，更新或删除）时传递事件的数据。   
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    [Serializable]
    public class EntityChangingEventData<TEntity>:EntityEventData<TEntity>
    {
        public EntityChangingEventData(TEntity entity)
            :base(entity)
        {

        }
    }
}
