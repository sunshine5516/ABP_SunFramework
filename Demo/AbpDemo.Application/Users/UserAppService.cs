using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Zero.Common.Authorization.Users;
using AbpDemo.Application.Roles.Dto;
using AbpDemo.Application.Users.Dto;
using AbpDemo.Core.Authorization.Roles;
using AbpDemo.Core.Authorization.Users;
using AbpFramework.Application.Services;
using AbpFramework.Application.Services.Dto;
using AbpFramework.Domain.Repositories;
using Microsoft.AspNet.Identity;

namespace AbpDemo.Application.Users
{
    public class UserAppService: AsyncCrudAppService<User, UserDto, long,
        PagedResultRequestDto, CreateUserDto, UpdateUserDto>, IUserAppService
    {
        #region 声明实例
        private readonly IRepository<User, long> _userRepository;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IRepository<Role> _roleRepository;
        //private readonly IRepository<UserClaim, long> _userClaimRepository;
        #endregion
        #region 构造函数
        public UserAppService(
            IRepository<User, long> userRepository,
            IRepository<User, long> repository,
            UserManager userManager, RoleManager roleManager,
            IRepository<Role> roleRepository)
            :base(repository)
        {
            this._userRepository = userRepository;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._roleRepository = roleRepository;
            //this._userClaimRepository = userClaimRepository;
        }
        #endregion
        #region 方法
        public List<User> GetAll()
        {
            return _userRepository.GetAllList();
        }
        [HttpGet]
        public ListResultDto<UserDto> GetUsers()
        {
            var users = _userRepository.GetAllList();
            return new ListResultDto<UserDto>(ObjectMapper.Map<List<UserDto>>(users));
        }
        public override async Task<UserDto> Create(CreateUserDto input)
        {
            //CheckCreatePermission();

            var user = ObjectMapper.Map<User>(input);

            user.TenantId = AbpSession.TenantId;
            user.Password = new PasswordHasher().HashPassword(input.Password);
            user.IsEmailConfirmed = true;

            //Assign roles
            user.Roles = new Collection<UserRole>();
            foreach (var roleName in input.RoleNames)
            {
                var role = await _roleManager.GetRoleByNameAsync(roleName);
                user.Roles.Add(new UserRole(AbpSession.TenantId, user.Id, role.Id));
            }

            await _userManager.CreateAsync(user);

            return MapToEntityDto(user);
        }
        public override async Task<UserDto> Update(UpdateUserDto input)
        {
            CheckUpdatePermission();
            var user = await _userManager.GetUserByIdAsync(input.Id);
            MapToEntity(input, user);
            if(input.RoleNames!=null)
            {
                await _userManager.SetRoles(user, input.RoleNames);
            }
            
            return await Get(input);
        }
        public async Task<ListResultDto<RoleDto>> GetRoles()
        {
            var roles = await _roleRepository.GetAllListAsync();
            return new ListResultDto<RoleDto>(ObjectMapper.Map<List<RoleDto>>(roles));
        }
        public override async Task Delete(EntityDto<long> input)
        {
            var user = await _userManager.GetUserByIdAsync(input.Id);
            await _userManager.DeleteAsync(user);
        }
        public override async Task<UserDto> Get(EntityDto<long> input)
        {
            var user = await base.Get(input);
            var userRole = await _userManager.GetRolesAsync(user.Id);
            user.Roles = userRole.Select(ur => ur).ToArray();
            return user;
        }
        #endregion
    }
}
