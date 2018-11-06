using Abp.MultiTenancy;
using AbpDemo.Core.Authorization.Users;

namespace AbpDemo.Core.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant() { }
        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
