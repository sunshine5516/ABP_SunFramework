using System;
using System.ComponentModel.DataAnnotations;

namespace AbpFramework.Application.Services.Dto
{
    [Serializable]
    public class PagedResultRequestDto : LimitedResultRequestDto, IPagedResultRequest
    {
        [Range(0, int.MaxValue)]
        public virtual int SkipCount { get; set; }
    }
}
