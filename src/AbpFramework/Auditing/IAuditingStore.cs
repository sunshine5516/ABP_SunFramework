using System.Threading.Tasks;

namespace AbpFramework.Auditing
{
    /// <summary>
    /// 定义持久化AuditInfo的方法
    /// 默认<see cref="SimpleLogAuditingStore"/>实现.
    /// </summary>
    public interface IAuditingStore
    {
        /// <summary>
        /// 保存持久化信息
        /// </summary>
        /// <param name="auditInfo"></param>
        /// <returns></returns>
        Task SaveAsync(AuditInfo auditInfo);
    }
}
