using System;
using System.Collections.Generic;
namespace AbpFramework.Application.Services.Dto
{
    /// <summary>
    /// 实现<see cref="IPagedResult{T}"/>接口.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class PagedResultDto<T> : ListResultDto<T>, IPagedResult<T>
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int TotalCount { get; set; }
        #region 构造函数
        public PagedResultDto()
        { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="totalCount">总数</param>
        /// <param name="items"></param>
        public PagedResultDto(int totalCount, IReadOnlyList<T> items)
            : base(items)
        {
            TotalCount = totalCount;
        }
        #endregion
    }
}
