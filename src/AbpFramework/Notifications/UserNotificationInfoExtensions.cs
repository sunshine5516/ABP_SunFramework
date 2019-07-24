namespace AbpFramework.Notifications
{
    public static class UserNotificationInfoExtensions
    {
        public static UserNotification ToUserNotification(this UserNotificationInfo userNotificationInfo, 
            TenantNotification tenantNotification)
        {
            return new UserNotification
            {
                Id = userNotificationInfo.Id,
                Notification = tenantNotification,
                UserId = userNotificationInfo.UserId,
                State = userNotificationInfo.State,
                TenantId = userNotificationInfo.TenantId
            };
        }
    }
}
