using AbpFramework.Collections;
namespace AbpFramework.Configuration.Startup
{
    public class SettingsConfiguration:ISettingsConfiguration
    {
        public ITypeList<SettingProvider> Providers { get; private set; }
        public SettingsConfiguration()
        {
            Providers = new TypeList<SettingProvider>();
        }
    }
}
