namespace AbpFramework.MultiTenancy
{
    public interface ITenantResolver
    {
        int? ResolveTenantId();
    }
}
