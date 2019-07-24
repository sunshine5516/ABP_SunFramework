using AbpFramework.Collections;
namespace AbpFramework.Configuration.Startup
{
    /// <summary>
    /// 用于集中化设置和管理SettingProvider的对象
    /// </summary>
    public interface ISettingsConfiguration
    {
        ITypeList<SettingProvider> Providers { get; }
    }
}
