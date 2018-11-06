using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Logging
{
    /// <summary>
    /// 定义<see cref ="Severity"/>属性的接口（请参阅<see cref ="LogSeverity"/>）
    /// </summary>
    public interface IHasLogSeverity
    {
        LogSeverity Severity { get; set; }
    }
}
