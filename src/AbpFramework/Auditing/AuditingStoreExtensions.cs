using AbpFramework.Threading;
namespace AbpFramework.Auditing
{
    public static class AuditingStoreExtensions
    {
        public static void Save(this IAuditingStore auditingStore,AuditInfo auditInfo)
        {
            AsyncHelper.RunSync(() => auditingStore.SaveAsync(auditInfo));
        }
    }
}
