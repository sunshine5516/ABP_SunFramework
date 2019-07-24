using System.Threading.Tasks;
namespace AbpFramework.Notifications
{
    public class NullRealTimeNotifier : IRealTimeNotifier
    {
        public static NullRealTimeNotifier Instance { get { return SingletonInstance; } }
        private static readonly NullRealTimeNotifier SingletonInstance = new NullRealTimeNotifier();
        public Task SendNotificationsAsync(UserNotification[] userNotifications)
        {
            return Task.FromResult(0);
        }
    }
}
