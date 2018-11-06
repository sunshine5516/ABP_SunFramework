namespace AbpFramework.Application.Services.Dto
{
    public interface ILimitedResultRequest
    {
        /// <summary>
        /// 最大结果数量
        /// </summary>
        int MaxResultCount { get; set; }
    }
}
