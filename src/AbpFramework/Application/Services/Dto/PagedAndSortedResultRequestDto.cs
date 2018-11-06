using System;
namespace AbpFramework.Application.Services.Dto
{
    /// <summary>
    /// 实现 <see cref="IPagedAndSortedResultRequest"/>.分页排序接口
    /// </summary>
    [Serializable]
    public class PagedAndSortedResultRequestDto : PagedResultRequestDto, IPagedAndSortedResultRequest
    {
        public virtual string Sorting { get; set; }
    }
}
