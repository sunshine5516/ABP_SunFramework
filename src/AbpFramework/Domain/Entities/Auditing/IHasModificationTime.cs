using System;
namespace AbpFramework.Domain.Entities.Auditing
{
    /// <summary>
    /// 如果必须存储此实体<see cref ="LastModificationTime"/>，实体可以实现此接口。
    /// 该接口封装了最后修改的时间
    /// </summary>
    public interface IHasModificationTime
    {
        /// <summary>
        /// 最后修改日期
        /// </summary>
        DateTime? LastModificationTime { get; set; }
    }
}
