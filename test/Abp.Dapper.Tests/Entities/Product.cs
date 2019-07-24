using AbpFramework.Domain.Entities;
using AbpFramework.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Abp.Dapper.Tests.Entities
{
    [Table("Products")]
    public class Product : FullAuditedEntity, IMayHaveTenant
    {
        protected Product()
        { }
        public Product(string name) : this()
        {
            Name = name;
        }
        [Required]
        public string Name { get; set; }
        public int? TenantId { get; set; }
    }
}
