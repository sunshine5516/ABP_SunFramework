namespace AbpFramework.Domain.Entities.Auditing
{
    /// <summary>
    /// 该接口添加了<see cref="IDeletionAudited"/> to <see cref="IAudited"/>
    /// 实现了实体的全面审计.
    /// </summary>
    public interface IFullAudited:IAudited, IDeletionAudited
    {

    }
    public interface IFullAudited<TUser> : IAudited<TUser>, IFullAudited, IDeletionAudited<TUser>
        where TUser : IEntity<long>
    {

    }
}
