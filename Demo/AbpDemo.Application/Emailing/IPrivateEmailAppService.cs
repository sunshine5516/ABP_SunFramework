using AbpDemo.Application.Emailing.Dto;
using AbpFramework.Application.Services;
using System.Threading.Tasks;

namespace AbpDemo.Application.Emailing
{
    public interface IPrivateEmailAppService : IApplicationService
    {
        Task Send(SendPrivateEmailInput input);
        string Send2();
    }
}
