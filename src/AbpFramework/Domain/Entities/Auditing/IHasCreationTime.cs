using System;
namespace AbpFramework.Domain.Entities.Auditing
{
    /// <summary>
    /// 封装了CreationTime
    /// 在将<see cref ='Entity'/>保存到数据库时自动设置。
    /// </summary>
    public interface IHasCreationTime
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreationTime { get; set; }
    }
}
