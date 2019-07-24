namespace AbpFramework.Notifications
{
    public class UserNotificationInfoWithNotificationInfo
    {
        public UserNotificationInfo UserNotification { get; set; }
        public TenantNotificationInfo Notification { get; set; }
        public UserNotificationInfoWithNotificationInfo(UserNotificationInfo userNotification, TenantNotificationInfo notification)
        {
            UserNotification = userNotification;
            Notification = notification;
        }
    }
}
