namespace AbpFramework.Application.Services.Dto
{
    /// <summary>
    /// 为标准化以请求分页结果接口
    /// </summary>
    public interface IPagedResultRequest: ILimitedResultRequest
    {
        int SkipCount { get; set; }
    }
}
