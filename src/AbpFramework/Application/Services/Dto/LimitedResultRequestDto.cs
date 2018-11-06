using System.ComponentModel.DataAnnotations;
namespace AbpFramework.Application.Services.Dto
{
    /// <summary>
    /// 实现<see cref="ILimitedResultRequest"/>接口.
    /// </summary>
    public class LimitedResultRequestDto : ILimitedResultRequest
    {
        [Range(1, int.MaxValue)]
        public virtual int MaxResultCount { get; set; } = 10;
    }
}
