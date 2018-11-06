using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Domain.Uow
{
    public static class AbpDataFilters
    {
        /// <summary>
        /// 软删除过滤器.
        /// 不获取“deleted”的数据
        /// </summary>
        public const string SoftDelete = "SoftDelete";
        /// <summary>
        /// 租户过滤器.
        /// 租户过滤器不获取不属于当前的租户
        /// </summary>
        public const string MustHaveTenant = "MustHaveTenant";

        /// <summary>
        /// 租户过滤器.
        /// 租户过滤器不获取不属于当前的租户
        /// </summary>
        public const string MayHaveTenant = "MayHaveTenant";

        /// <summary>
        /// 参数.
        /// </summary>
        public static class Parameters
        {
            /// <summary>
            /// "tenantId".
            /// </summary>
            public const string TenantId = "tenantId";
        }
    }
}
