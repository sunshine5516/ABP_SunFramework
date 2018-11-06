namespace AbpFramework.Auditing
{
    /// <summary>
    /// 提供一个接口来提供上层的审计信息。
    /// ABP核心模块处于最底层，有些上层的信息在这一层无法直接取得（比如浏览器信息）。
    /// ABP的做法是在上层实现IAuditInfoProvider，然后将其register到底层的容器中。
    /// 处于底层ABP的核心模块则从resolve出这个对象，然后调用该对象的fill方法来完善AuditInfo。
    /// </summary>
    public interface IAuditInfoProvider
    {
        void Fill(AuditInfo auditInfo);
    }
}
