using AbpFramework.Domain.Entities;
using System.Threading.Tasks;
namespace AbpFramework.Notifications
{
    /// <summary>
    /// 用于发布Notification，首先调用INotificationStore实例进行实例化，接着分发Notification。
    /// 如果有接收者并且接收者少于5个则直接调用INotificationDistributer进行分发，否则就把分发的任务加到后台工作队列中去。
    /// </summary>
    public interface INotificationPublisher
    {
        /// <summary>
        /// 发布新的通知.
        /// </summary>
        /// <param name="notificationName">通知唯一名称</param>
        /// <param name="data">通知内容(可选)</param>
        /// <param name="entityIdentifier"></param>
        /// <param name="severity">通知等级y</param>
        /// <param name="userIds">目标用户ID，用于向特定用户发送通知（不检查订阅），如果为空，则向所有已经订阅的用户发送</param>
        /// <param name="excludedUserIds">被排除的用户ID，可以将此设置为在向订阅用户发布通知时排除某些用户。</param>
        /// <param name="tenantIds">目标租户ID，用于向特定租户的订阅用户发送通知。如果userIds被设置了，则不设置该字段
        /// </param>
        Task PublishAsync(
            string notificationName,
            NotificationData data = null,
            EntityIdentifier entityIdentifier = null,
            NotificationSeverity severity = NotificationSeverity.Info,
            UserIdentifier[] userIds = null,
            UserIdentifier[] excludedUserIds = null,
            int?[] tenantIds = null);
    }
}
