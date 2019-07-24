using AbpFramework.Dependency;
using AbpFramework.Extensions;
using JetBrains.Annotations;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
namespace AbpFramework.RealTime
{
    public class OnlineClientManager : IOnlineClientManager, ISingletonDependency
    {
        #region 声明实例
        public event EventHandler<OnlineClientEventArgs> ClientConnected;
        public event EventHandler<OnlineClientEventArgs> ClientDisconnected;
        public event EventHandler<OnlineUserEventArgs> UserConnected;
        public event EventHandler<OnlineUserEventArgs> UserDisconnected;
        protected ConcurrentDictionary<string, IOnlineClient> Clients { get; }
        protected readonly object SyncObj = new object();
        #endregion
        #region 构造函数
        public OnlineClientManager()
        {
            Clients = new ConcurrentDictionary<string, IOnlineClient>();
        }
        #endregion
        #region 方法
        public virtual void Add(IOnlineClient client)
        {
            lock(SyncObj)
            {
                var userWasAlreadyOnline = false;
                var user=client.ToUserIdentifierOrNull();
                if(user!=null)
                {
                    userWasAlreadyOnline = this.IsOnline(user);
                }
                Clients[client.ConnectionId] = client;
                ClientConnected.InvokeSafely(this, new OnlineClientEventArgs(client));

                if (user != null && !userWasAlreadyOnline)
                {
                    UserConnected.InvokeSafely(this, new OnlineUserEventArgs(user, client));
                }
            }
        }
        [NotNull]
        public virtual IReadOnlyList<IOnlineClient> GetAllByUserId([NotNull] IUserIdentifier user)
        {
            Check.NotNull(user, nameof(user));
            //return GetAllClients()
            //    .Where(c=>(c.UserId==user.UserId&&c.TenantId==user.TenantId))
            //    .ToImmutableList();
            return GetAllClients()
                .Where(c => (c.UserId == user.UserId))
                .ToImmutableList();
        }

        public virtual IReadOnlyList<IOnlineClient> GetAllClients()
        {
            lock(SyncObj)
            {
                return Clients.Values.ToImmutableList();
            }
        }

        public virtual IOnlineClient GetByConnectionIdOrNull(string connectionId)
        {
            lock (SyncObj)
            {
                return Clients.GetOrDefault(connectionId);
            }
        }

        public bool Remove(string connectionId)
        {
            lock (SyncObj)
            {
                IOnlineClient client;
                var isRemoved = Clients.TryRemove(connectionId, out client);
                if (isRemoved)
                {
                    var user = client.ToUserIdentifierOrNull();
                    if(user!=null&&!this.IsOnline(user))
                    {
                        UserDisconnected.InvokeSafely(this, new OnlineUserEventArgs(user, client));
                    }
                    ClientDisconnected.InvokeSafely(this, new OnlineClientEventArgs(client));
                }
                return isRemoved;
            }
        }
        #endregion


    }
}
