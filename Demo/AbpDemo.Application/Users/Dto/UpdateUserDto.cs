﻿using Abp.AutoMapper.AutoMapper;
using Abp.Zero.Common.Authorization.Users;
using AbpDemo.Core.Authorization.Users;
using AbpFramework.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
namespace AbpDemo.Application.Users.Dto
{
    [AutoMapTo(typeof(User))]
    public class UpdateUserDto : EntityDto<long>
    {
        [Required]
        [StringLength(AbpUserBase.MaxUserNameLength)]
        public string UserName { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxSurnameLength)]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        public bool IsActive { get; set; }

        public string[] RoleNames { get; set; }
    }
}
