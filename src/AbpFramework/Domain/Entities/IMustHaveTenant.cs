using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Domain.Entities
{
    public interface IMustHaveTenant
    {
        int TenantId { get; set; }
    }
}
