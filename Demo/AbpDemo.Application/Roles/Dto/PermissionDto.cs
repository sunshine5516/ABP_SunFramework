using Abp.AutoMapper.AutoMapper;
using AbpFramework.Application.Services.Dto;
using AbpFramework.Authorization;
namespace AbpDemo.Application.Roles.Dto
{
    [AutoMapFrom(typeof(Permission))]
    public class PermissionDto : EntityDto<long>
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
    }
}
