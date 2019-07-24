using AbpFramework.Collections;
namespace Abp.Zero.Common.Zero.Configuration
{
    /// <summary>
    /// 用户管理配置
    /// </summary>
    public interface IUserManagementConfig
    {
        ITypeList<object> ExternalAuthenticationSources { get; set; }
    }
}
