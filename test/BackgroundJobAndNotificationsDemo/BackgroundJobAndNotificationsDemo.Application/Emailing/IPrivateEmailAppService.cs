using AbpFramework.Application.Services;
using BackgroundJobAndNotificationsDemo.Application.Emailing.Dto;

namespace BackgroundJobAndNotificationsDemo.Application.Emailing
{
    public interface IPrivateEmailAppService : IApplicationService
    {
        void Send(SendPrivateEmailInput input);
    }
}
