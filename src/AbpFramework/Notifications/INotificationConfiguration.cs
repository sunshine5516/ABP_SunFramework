using AbpFramework.Collections;
namespace AbpFramework.Notifications
{
    /// <summary>
    /// notification配置.
    /// </summary>
    public interface INotificationConfiguration
    {
        ITypeList<NotificationProvider> Providers { get; }
    }
}
