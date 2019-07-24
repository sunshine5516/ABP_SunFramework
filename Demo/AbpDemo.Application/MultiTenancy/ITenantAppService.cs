using AbpDemo.Application.MultiTenancy.Dto;
using AbpFramework.Application.Services;
using AbpFramework.Application.Services.Dto;

namespace AbpDemo.Application.MultiTenancy
{
    public interface ITenantAppService: IAsyncCrudAppService<TenantDto, int, 
        PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
