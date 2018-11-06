using AbpFramework.Domain.Entities;
using AbpFramework.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
namespace Abp.Zero.Common.Authorization.Users
{
    [Table("AbpUserClaims")]
    public class UserClaim : CreationAuditedEntity<long>, IMayHaveTenant
    {
        public virtual int? TenantId { get; set; }
        public virtual long UserId { get; set; }
        public virtual string ClaimType { get; set; }
        public virtual string ClaimValue { get; set; }
        public UserClaim() { }
        public UserClaim(AbpUserBase user,Claim claim)
        {
            TenantId = user.TenantId;
            UserId = user.Id;
            ClaimType = claim.Type;
            ClaimValue = claim.Value;
        }
    }
}
