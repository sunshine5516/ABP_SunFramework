using Castle.Core.Logging;
using System.Threading.Tasks;

namespace AbpFramework.Auditing
{
    /// <summary>
    /// 实现<see cref="IAuditingStore"/>接口存储审计信息.
    /// </summary>
    public class SimpleLogAuditingStore : IAuditingStore
    {
        /// <summary>
        /// 单例
        /// </summary>
        public static SimpleLogAuditingStore Instance { get; } = new SimpleLogAuditingStore();
        public ILogger Logger { get; set; }
        public SimpleLogAuditingStore()
        {
            Logger = NullLogger.Instance;
        }
        public Task SaveAsync(AuditInfo auditInfo)
        {
            if (auditInfo.Exception == null)
            {
                Logger.Info(auditInfo.ToString());
            }
            else
            {
                Logger.Warn(auditInfo.ToString());
            }
            return Task.FromResult(0);
        }
    }
}
