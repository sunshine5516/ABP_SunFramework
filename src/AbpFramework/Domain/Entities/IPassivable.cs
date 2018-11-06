namespace AbpFramework.Domain.Entities
{
    /// <summary>
    /// 实体是否激活接口
    /// </summary>
    public interface IPassivable
    {
        /// <summary>
        /// True: 该实体是激活状态.
        /// False: 该实体是未激活状态.
        /// </summary>
        bool IsActive { get; set; }
    }
}
