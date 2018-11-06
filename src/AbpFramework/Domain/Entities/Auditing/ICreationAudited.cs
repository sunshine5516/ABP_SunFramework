namespace AbpFramework.Domain.Entities.Auditing
{
    /// <summary>
    /// 封装了CreatorUserId，这个是long类型
    /// 当将<see cref ="Entity"/>保存到数据库时，会自动设置创建时间和创建者用户。
    /// </summary>
    public interface ICreationAudited:IHasCreationTime
    {
        /// <summary>
        /// 创建者ID
        /// </summary>
        long? CreatorUserId { get; set; }
    }
    public interface ICreationAudited<TUser> : ICreationAudited
        where TUser : IEntity<long>
    {
        /// <summary>
        /// 创建该类型的用户
        /// </summary>
        TUser CreatorUser { get; set; }
    }
}
