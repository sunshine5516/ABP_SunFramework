using System.Threading.Tasks;
using AbpDemo.Application.Configuration.Dto;

namespace AbpDemo.Application.Configuration
{
    public class ConfigurationAppService : ABPMultiMVCAppServiceBase,IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            //await SettingManager.ChangeSettingForUserAsync
        }
    }
}
