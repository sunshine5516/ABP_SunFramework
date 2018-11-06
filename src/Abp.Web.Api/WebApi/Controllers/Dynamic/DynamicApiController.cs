using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.WebApi.Controllers.Dynamic
{
    /// <summary>
    /// 用作所有动态生成的ApiController的基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>
    /// 动态ApiController用于将对象（通常为Application Service类）透明地显示给远程客户端。
    /// </remarks>
    public class DynamicApiController<T>: AbpApiController, IDynamicApiController
    {
        public List<string> AppliedCrossCuttingConcerns { get; }
        public DynamicApiController()
        {
            AppliedCrossCuttingConcerns = new List<string>();
        }
    }
}
