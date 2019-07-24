using Abp.AutoMapper.AutoMapper;
using AbpDemo.Core.Authorization.Users;
using AbpFramework.Application.Services.Dto;
namespace AbpDemo.Application.Sessions.Dto
{
    [AutoMapFrom(typeof(User))]
    public class UserLoginInfoDto : EntityDto<long>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
    }
}
