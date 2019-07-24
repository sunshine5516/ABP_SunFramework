using Abp.AutoMapper.AutoMapper;
using Abp.MultiTenancy;
using AbpDemo.Core.MultiTenancy;
using AbpFramework.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
namespace AbpDemo.Application.MultiTenancy.Dto
{
    [AutoMapTo(typeof(Tenant)), AutoMapFrom(typeof(Tenant))]
    public class TenantDto:EntityDto
    {
        [Required]
        [StringLength(AbpTenantBase.MaxTenancyNameLength)]
        [RegularExpression(Tenant.TenancyNameRegex)]
        public string TenancyName { get; set; }
        [Required]
        [StringLength(Tenant.MaxNameLength)]
        public string Name { get; set; }       
        public bool IsActive { get; set; }
    }
}
