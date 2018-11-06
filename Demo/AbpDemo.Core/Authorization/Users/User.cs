using Abp.Zero.Authorization.Users;
using AbpFramework.Extensions;
using System;
namespace AbpDemo.Core.Authorization.Users
{
    public  class User : AbpUser<User>
    {
        public const string DefaultPassword = "123qwe";

        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        public static User CreateTenantAdminUser(int tenantId, string emailAddress, string password)
        {
            var user = new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
                Surname = AdminUserName,
                EmailAddress = emailAddress,
                //Password = new PasswordHasher().HashPassword(password)
                Password ="123"
            };

            return user;
        }
    }
}
