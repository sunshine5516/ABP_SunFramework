using AbpDemo.Application.Roles.Dto;
using AbpDemo.Application.Users.Dto;
using AbpDemo.Core.Authorization.Users;
using AbpFramework.Application.Services;
using AbpFramework.Application.Services.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AbpDemo.Application.Users
{
    public interface IUserAppService: IApplicationService,IAsyncCrudAppService<UserDto,
        long, PagedResultRequestDto, CreateUserDto, UpdateUserDto>
    {
        List<User> GetAll();
        //Task<ListResultDto<RoleDto>> GetRoles();
        ListResultDto<UserDto> GetUsers();
        Task<ListResultDto<RoleDto>> GetRoles();
    }
}
