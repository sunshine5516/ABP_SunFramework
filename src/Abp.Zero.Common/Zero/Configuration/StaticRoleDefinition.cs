
using AbpFramework.MultiTenancy;

namespace Abp.Zero.Common.Zero.Configuration
{
    public class StaticRoleDefinition
    {
        public string RoleName { get; set; }
        public MultiTenancySides Side { get; private set; }
        public StaticRoleDefinition(string roleName,MultiTenancySides side)
        {
            RoleName = roleName;
            Side = side;
        }
    }
}
