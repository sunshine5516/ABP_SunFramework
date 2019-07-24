using JetBrains.Annotations;
using System;
using System.Collections.Generic;
namespace AbpFramework.RealTime
{
    /// <summary>
    /// 用于管理连接到应用程序的在线客户端。
    /// </summary>
    public interface IOnlineClientManager
    {
        event EventHandler<OnlineClientEventArgs> ClientConnected;
        event EventHandler<OnlineClientEventArgs> ClientDisconnected;
        event EventHandler<OnlineUserEventArgs> UserConnected;

        event EventHandler<OnlineUserEventArgs> UserDisconnected;
        void Add(IOnlineClient client);
        bool Remove(string connectionId);
        IOnlineClient GetByConnectionIdOrNull(string connectionId);
        IReadOnlyList<IOnlineClient> GetAllClients();

        IReadOnlyList<IOnlineClient> GetAllByUserId([NotNull] IUserIdentifier user);
    }
}
