using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Notifications
{
    /// <summary>
    /// 向用户发送实时通知接口
    /// </summary>
    public interface IRealTimeNotifier
    {
        Task SendNotificationsAsync(UserNotification[] userNotifications);
    }
}
