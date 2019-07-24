using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Zero.Common.Authorization.Users;
using Abp.Zero.IdentityFramework;
using AbpDemo.Application.Roles.Dto;
using AbpDemo.Core.Authorization.Roles;
using AbpDemo.Core.Authorization.Users;
using AbpFramework.Application.Services;
using AbpFramework.Application.Services.Dto;
using AbpFramework.Domain.Repositories;
using AbpFramework.UI;
using Microsoft.AspNet.Identity;

namespace AbpDemo.Application.Roles
{
    public class RoleAppService : AsyncCrudAppService<Role, RoleDto,
        int, PagedResultRequestDto, CreateRoleDto, RoleDto>, IRoleAppService
    {
        #region 声明实例
        private readonly RoleManager _roleManager;
        private readonly UserManager _userManager;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<UserRole, long> _userRoleRepository;
        private readonly IRepository<User, long> _userRepository;
        #endregion
        #region 构造函数
        public RoleAppService(
         IRepository<Role> repository,
         RoleManager roleManager,
         UserManager userManager,
         IRepository<User, long> userRepository,
         IRepository<UserRole, long> userRoleRepository,
         IRepository<Role> roleRepository)
         : base(repository)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
        }
        #endregion
        #region 方法
        public override async Task<RoleDto> Create(CreateRoleDto input)
        {
            CheckCreatePermission();
            var role = ObjectMapper.Map<Role>(input);
            //CheckErrors();
            await _roleManager.CreateAsync(role);
            var grantedPermissions = PermissionManager
                .GetAllPermissions()
                .Where(p => input.Permissions.Contains(p.Name))
                .ToList();
            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);
            return MapToEntityDto(role);
        }

        public override async Task Delete(EntityDto<int> input)
        {
            var role = await _roleManager.FindByIdAsync(input.Id);
            if (role.IsStatic)
            {
                throw new UserFriendlyException("CannotDeleteAStaticRole");
            }
            var users=await GetUsersInRoleAsync(role.Name);
            foreach(var user in users)
            {
                CheckErrors(await _userManager.RemoveFromRoleAsync(user, role.Name));
            }
            CheckErrors(await _roleManager.DeleteAsync(role));
            
        }

        private Task<List<long>> GetUsersInRoleAsync(string roleName)
        {
            var users = (from user in _userRepository.GetAll()
                         join userRole in _userRoleRepository.GetAll() on user.Id equals userRole.UserId
                         join role in _roleRepository.GetAll() on userRole.RoleId equals role.Id
                         where role.Name == roleName
                         select user.Id).Distinct().ToList();
            return Task.FromResult(users);
        }

        //public override async Task<RoleDto> Get(EntityDto<int> input)
        //{
        //    var role =await base.Get(input);
        //    //role.Permissions= role.Permissions.Select(ur => ur).ToArray();
        //    //var permission = await _roleManager.GetGrantedPermissionsAsync(role);
        //    return role;
        //}
        protected override Task<Role> GetEntityByIdAsync(int id)
        {
            var role = Repository.GetAllIncluding(x => x.Permissions).FirstOrDefault(x => x.Id == id);
            return Task.FromResult(role);
        }

        public Task<ListResultDto<PermissionDto>> GetAllPermissions()
        {
            var permissions = PermissionManager.GetAllPermissions();
            return Task.FromResult(new ListResultDto<PermissionDto>(
                ObjectMapper.Map<List<PermissionDto>>(permissions)));
        }

        public override async Task<RoleDto> Update(RoleDto input)
        {
            CheckUpdatePermission();
            var role = await _roleManager.GetRoleByIdAsync(input.Id);
            ObjectMapper.Map(input, role);
            CheckErrors(await _roleManager.UpdateAsync(role));
            var grantedPermissions = PermissionManager.GetAllPermissions()
                .Where(p => input.Permissions.Contains(p.Name))
                .ToList();
            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);
            return MapToEntityDto(role);
        }
        #endregion
        protected virtual void CheckErrors(IdentityResult identityResul)
        {
            identityResul.CheckErrors();
        }

    }
}
