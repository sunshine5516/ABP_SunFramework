using Abp.Web.SignalR.Web.SignalR.Hubs;
using AbpFramework;
using AbpFramework.Dependency;
using AbpFramework.Notifications;
using AbpFramework.RealTime;
using Castle.Core.Logging;
using Microsoft.AspNet.SignalR;
using System;
using System.Threading.Tasks;

namespace Abp.Web.SignalR.Web.SignalR.Notifications
{
    public class SignalRRealTimeNotifier : IRealTimeNotifier, ITransientDependency
    {
        #region 声明实例
        public ILogger Logger { get; set; }
        private readonly IOnlineClientManager _onlineClientManager;
        private static IHubContext CommonHub
        {
            get
            {
                return GlobalHost.ConnectionManager.GetHubContext<AbpCommonHub>();
            }
        }
        #endregion
        #region 构造函数
        public SignalRRealTimeNotifier(IOnlineClientManager onlineClientManager)
        {
            _onlineClientManager = onlineClientManager;
            Logger = NullLogger.Instance;
        }
        #endregion
        public Task SendNotificationsAsync(UserNotification[] userNotifications)
        {
            foreach(var userNotification in userNotifications)
            {
                try
                {
                    var onlineClients = _onlineClientManager.GetAllByUserId(userNotification);
                    foreach(var onlineClient in onlineClients)
                    {
                        var signalRClient = CommonHub.Clients.Client(onlineClient.ConnectionId);
                        if(signalRClient==null)
                        {
                            Logger.Debug("Can not get user " + userNotification.ToUserIdentifier() + " with connectionId " + onlineClient.ConnectionId + " from SignalR hub!");
                            continue;
                        }
                        signalRClient.getNotification(userNotification);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Warn("Could not send notification to user: " + userNotification.ToUserIdentifier());
                    Logger.Warn(ex.ToString(), ex);
                }
            }
            return Task.FromResult(0);
        }
    }
}
