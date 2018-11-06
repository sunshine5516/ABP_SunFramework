using System;
namespace AbpFramework.Domain.Entities.Auditing
{
    /// <summary>
    /// 封装了DeletionTime
    /// </summary>
    public interface IHasDeletionTime : ISoftDelete
    {
        /// <summary>
        /// 删除时间
        /// </summary>
        DateTime? DeletionTime { get; set; }
    }
}
