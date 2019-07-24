using System;
namespace AbpFramework.Notifications
{
    [Serializable]
    public class NotificationDistributionJobArgs
    {
        public Guid NotificationId { get; set; }
        public NotificationDistributionJobArgs(Guid notificationId)
        {
            NotificationId = notificationId;
        }
    }
}
