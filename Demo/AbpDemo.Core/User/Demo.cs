
using AbpFramework.Domain.Entities.Auditing;
using System;

namespace AbpDemo.Core
{
    public  class Demo: FullAuditedEntity<long>
    {
        public const string DefaultPassword = "123qwe";        
        public int TenantId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
         public string Surname { get; set; }
        public string EmailAddress { get; set; }

        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N");
        }

        public static Demo CreateTenantAdminUser(int tenantId, string emailAddress, string password)
        {
            var user = new Demo
            {
                TenantId = tenantId,
                
                EmailAddress = emailAddress,
               
            };

            return user;
        }
    }
}
