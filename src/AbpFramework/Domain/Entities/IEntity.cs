namespace AbpFramework.Domain.Entities
{
    /// <summary>
    /// <see cref ="IEntity {TPrimaryKey}"/>的快捷方式，用于最常用的主键类型（<see cref ="int"/>）。
    /// </summary>
    public interface IEntity:IEntity<int>
    {
    }
}
