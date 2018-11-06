using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Domain.Uow
{
    /// <summary>
    /// 在需要数据库连接时获取连接字符串。
    /// </summary>
    public interface IConnectionStringResolver
    {
        string GetNameOrConnectionString(ConnectionStringResolveArgs args);
    }
}
