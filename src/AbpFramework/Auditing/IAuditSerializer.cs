namespace AbpFramework.Auditing
{
    public interface IAuditSerializer
    {
        string Serialize(object obj);
    }
}
