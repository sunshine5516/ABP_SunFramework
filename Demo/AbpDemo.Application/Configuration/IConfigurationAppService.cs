using AbpDemo.Application.Configuration.Dto;
using AbpFramework.Application.Services;
using System.Threading.Tasks;
namespace AbpDemo.Application.Configuration
{
    public interface IConfigurationAppService: IApplicationService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
