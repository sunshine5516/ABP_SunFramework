namespace AbpFramework.Notifications
{
    public static class UserNotificationInfoWithNotificationInfoExtensions
    {
        public static UserNotification ToUserNotification
            (this UserNotificationInfoWithNotificationInfo userNotificationInfoWithNotificationInfo)
        {
            return userNotificationInfoWithNotificationInfo.UserNotification.ToUserNotification(
                userNotificationInfoWithNotificationInfo.Notification.ToTenantNotification());
        }
    }
}
