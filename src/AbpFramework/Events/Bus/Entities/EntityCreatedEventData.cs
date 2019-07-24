namespace AbpFramework.Events.Bus.Entities
{
    /// <summary>
    /// 此类事件可用于在创建实体后立即通知。
    /// </summary>
    public class EntityCreatedEventData<TEntity> : EntityChangedEventData<TEntity>
    {
        /// <summary>
        /// 构造函数.
        /// </summary>
        /// <param name="entity">The entity which is created</param>
        public EntityCreatedEventData(TEntity entity)
            : base(entity)
        {

        }
    }
}
