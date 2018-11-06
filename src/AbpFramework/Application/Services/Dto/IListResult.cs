using System.Collections.Generic;
namespace AbpFramework.Application.Services.Dto
{
    /// <summary>
    /// 封装了一个IReadOnlyList集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IListResult<T>
    {
        IReadOnlyList<T> Items { get; set; }
    }
}
