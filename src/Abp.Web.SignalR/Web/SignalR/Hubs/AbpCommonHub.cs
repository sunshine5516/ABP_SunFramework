using AbpFramework.Dependency;
using AbpFramework.RealTime;
using AbpFramework.Runtime.Session;
using Castle.Core.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Abp.Web.SignalR.Web.SignalR.Hubs
{
    public class AbpCommonHub : AbpHubBase, ITransientDependency
    {
        private readonly IOnlineClientManager _onlineClientManager;
        #region 构造函数
        public AbpCommonHub(IOnlineClientManager onlineClientManager)
        {
            _onlineClientManager = onlineClientManager;

            Logger = NullLogger.Instance;
            AbpSession = NullAbpSession.Instance;
        }
        #endregion
        #region 方法
        public void Register()
        {
            Logger.Debug("A client is registered: " + Context.ConnectionId);
        }
        public override async  Task OnConnected()
        {
            await base.OnConnected();
            var client = CreateClientForCurrentConnection();
            Logger.Debug("A client is connected: " + client);
            _onlineClientManager.Add(client);
        }
        public override async Task OnReconnected()
        {
            await base.OnReconnected();
            var client = _onlineClientManager.GetByConnectionIdOrNull(Context.ConnectionId);
            if(client==null)
            {
                client = CreateClientForCurrentConnection();
                _onlineClientManager.Add(client);
                Logger.Debug("A client is connected (on reconnected event): " + client);
            }
            else
            {
                Logger.Debug("A client is reconnected: " + client);
            }
        }
        public override async Task OnDisconnected(bool stopCalled)
        {
            await base.OnDisconnected(stopCalled);
            Logger.Debug("A client is disconnected: " + Context.ConnectionId);
            try
            {
                _onlineClientManager.Remove(Context.ConnectionId);
            }
            catch (Exception ex)
            {
                Logger.Warn(ex.ToString(), ex);
            }
        }
        #endregion
        #region 辅助方法
        private IOnlineClient CreateClientForCurrentConnection()
        {
            return new OnlineClient(
                Context.ConnectionId,
                GetIpAddressOfClient(),
                AbpSession.TenantId,
                AbpSession.UserId
            );
        }

        private string GetIpAddressOfClient()
        {
            try
            {
                return Context.Request.Environment["server.RemoteIpAddress"].ToString();
            }
            catch (Exception ex)
            {
                Logger.Error("Can not find IP address of the client! connectionId: " + Context.ConnectionId);
                Logger.Error(ex.Message, ex);
                return "";
            }
        }
        #endregion

    }
}
