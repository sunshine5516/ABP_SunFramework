using Abp.AutoMapper.AutoMapper;
using AbpDemo.Core.MultiTenancy;
namespace AbpDemo.Application.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto
    {
        public string TenancyName { get; set; }
        public string Name { get; set; }
    }
}
