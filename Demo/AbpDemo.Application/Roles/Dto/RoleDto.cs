using Abp.AutoMapper.AutoMapper;
using Abp.Zero.Common.Authorization.Roles;
using AbpDemo.Core.Authorization.Roles;
using AbpFramework.Application.Services.Dto;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace AbpDemo.Application.Roles.Dto
{
    [AutoMapFrom(typeof(Role)),AutoMapTo(typeof(Role))]
    public class RoleDto:EntityDto<int>
    {
        [Required]
        [StringLength(AbpRoleBase.MaxNameLength)]
        public string Name { get; set; }
        [Required]
        [StringLength(AbpRoleBase.MaxDisplayNameLength)]
        public string DisplayName { get; set; }
        [StringLength(Role.MaxDescriptionLength)]
        public string Description { get; set; }
        public bool IsStatic { get; set; }
        public List<string> Permissions { get; set; }
    }
}
