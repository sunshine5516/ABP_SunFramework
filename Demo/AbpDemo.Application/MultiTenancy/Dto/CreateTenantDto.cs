using Abp.AutoMapper.AutoMapper;
using Abp.MultiTenancy;
using Abp.Zero.Common.Authorization.Users;
using AbpDemo.Core.MultiTenancy;
using AbpFramework.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
namespace AbpDemo.Application.MultiTenancy.Dto
{
    [AutoMapTo(typeof(Tenant))]
    public class CreateTenantDto:EntityDto
    {
        [Required]
        [StringLength(AbpTenantBase.MaxTenancyNameLength)]
        [RegularExpression(Tenant.TenancyNameRegex)]
        public string TenancyName { get; set; }
        [Required]
        [StringLength(Tenant.MaxNameLength)]
        public string Name { get; set; }
        [Required]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string AdminEmailAddress { get; set; }
        [MaxLength(AbpTenantBase.MaxConnectionStringLength)]
        public string ConnectionString { get; set; }
        public bool IsActive { get; set; }
    }
}
