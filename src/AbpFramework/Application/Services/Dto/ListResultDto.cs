using System;
using System.Collections.Generic;
namespace AbpFramework.Application.Services.Dto
{
    /// <summary>
    /// 实现<see cref="IListResult{T}"/>接口.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class ListResultDto<T> : IListResult<T>
    {
        private IReadOnlyList<T> _items;
        public IReadOnlyList<T> Items
        {
            get { return _items?? (_items = new List<T>()); }
            set { _items = value; }
        }
        public ListResultDto()
        {

        }
        public ListResultDto(IReadOnlyList<T> items)
        {
            Items = items;
        }
    }
}
