using AbpFramework.Auditing;
using AbpFramework.Dependency;
using AbpFramework.Domain.Repositories;
using System.Threading.Tasks;
namespace Abp.Zero.Common.Auditing
{
    public class AuditingStore : IAuditingStore, ITransientDependency
    {
        #region 声明实例
        private readonly IRepository<AuditLog, long> _auditLogRepository;
        #endregion
        #region 构造函数
        public AuditingStore(IRepository<AuditLog, long> auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }
        #endregion
        #region 方法
        public Task SaveAsync(AuditInfo auditInfo)
        {
            return _auditLogRepository.InsertAsync(AuditLog.CreateFromAuditInfo(auditInfo));
        }
        #endregion

    }
}
