namespace AbpFramework.Application.Services.Dto
{
    /// <summary>
    /// 封装了TotalCount属性
    /// </summary>
    public interface IHasTotalCount
    {
        /// <summary>
        /// 总数
        /// </summary>
        int TotalCount { get; set; }
    }
}
