namespace AbpFramework.Application.Services.Dto
{
    /// <summary>
    /// 基于Entity的DTO,只支持int类型的主键。
    /// </summary>
    public interface IEntityDto
    {
    }
    /// <summary>
    /// 基于Entity的DTO,支持泛型类型的主键。
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public interface IEntityDto<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
    }
}
