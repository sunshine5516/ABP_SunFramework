using Abp.MultiTenancy;
using BackgroundJobAndNotificationsDemo.Core.Users;
namespace BackgroundJobAndNotificationsDemo.Core.MultiTenancy
{
    public class Tenant: AbpTenant<User>
    {
        public Tenant() { }
        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
