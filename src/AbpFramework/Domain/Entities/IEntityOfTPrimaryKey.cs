namespace AbpFramework.Domain.Entities
{
    /// <summary>
    /// 定义基本实体类型接口，所有实体需要继承该接口
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public interface IEntity<TPrimaryKey>
    {
        /// <summary>
        /// 实体ID
        /// </summary>
        TPrimaryKey Id { get; set; }
        bool IsTransient();
    }
}
