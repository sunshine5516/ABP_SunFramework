using AbpFramework.Collections;
namespace Abp.Zero.Common.Zero.Configuration
{
    public class UserManagementConfig : IUserManagementConfig
    {
        public ITypeList<object> ExternalAuthenticationSources { get; set; }
        public UserManagementConfig()
        {
            ExternalAuthenticationSources = new TypeList();
        }
    }
}
