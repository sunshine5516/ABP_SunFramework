using System;
using System.Collections.Generic;
namespace AbpFramework.RealTime
{
    /// <summary>
    /// 连接到应用程序的在线客户端
    /// </summary>
    public interface IOnlineClient
    {
        string ConnectionId { get; }
        string IpAddress { get; }
        int? TenantId { get; }
        long? UserId { get; }
        DateTime ConnectTime { get; }
        object this[string key] { get;set; }
        Dictionary<string,object> Properties { get; }
    }
}
