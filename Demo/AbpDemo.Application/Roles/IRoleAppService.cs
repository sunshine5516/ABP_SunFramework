using AbpDemo.Application.Roles.Dto;
using AbpFramework.Application.Services;
using AbpFramework.Application.Services.Dto;
using System.Threading.Tasks;

namespace AbpDemo.Application.Roles
{
    public interface IRoleAppService: IAsyncCrudAppService<RoleDto,int, PagedResultRequestDto
        , CreateRoleDto, RoleDto>
    {
        Task<ListResultDto<PermissionDto>> GetAllPermissions();
    }
}
