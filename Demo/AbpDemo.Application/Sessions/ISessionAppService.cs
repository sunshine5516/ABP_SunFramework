using AbpDemo.Application.Sessions.Dto;
using AbpFramework.Application.Services;
using System.Threading.Tasks;

namespace AbpDemo.Application.Sessions
{
    public interface ISessionAppService: IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
