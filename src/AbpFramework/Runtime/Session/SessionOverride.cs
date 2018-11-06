using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Runtime.Session
{
    public class SessionOverride
    {
        public long? UserId { get; }

        public int? TenantId { get; }

        public SessionOverride(int? tenantId, long? userId)
        {
            TenantId = tenantId;
            UserId = userId;
        }
    }
}
