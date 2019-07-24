using AbpFramework.Collections;
namespace AbpFramework.Notifications
{
    internal class NotificationConfiguration : INotificationConfiguration
    {
        public ITypeList<NotificationProvider> Providers { get; set; }
        public NotificationConfiguration()
        {
            Providers = new TypeList<NotificationProvider>();
        }
    }
}
