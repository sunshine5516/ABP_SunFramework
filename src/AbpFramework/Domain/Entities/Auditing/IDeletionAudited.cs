namespace AbpFramework.Domain.Entities.Auditing
{
    /// <summary>
    /// 封装了DeleterUserId，这个是long类型
    /// </summary>
    public interface IDeletionAudited : IHasDeletionTime
    {
        /// <summary>
        /// 删除用户ID
        /// </summary>
        long? DeleterUserId { get; set; }
    }
    public interface IDeletionAudited<TUser> : IDeletionAudited
        where TUser : IEntity<long>
    {
        /// <summary>
        /// 删除此实体类型的用户
        /// </summary>
        TUser DeleterUser { get; set; }
    }
}
