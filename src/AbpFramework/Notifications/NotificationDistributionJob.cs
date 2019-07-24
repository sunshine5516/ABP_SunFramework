using AbpFramework.BackgroundJobs;
using AbpFramework.Dependency;
using AbpFramework.Threading;
namespace AbpFramework.Notifications
{
    /// <summary>
    /// 封装了INotificationDistributer的后台任务，当Notification的接收者超过5人时会，ABP将分发任务封装为一个后台执行任务，以减少用户等待时间.
    /// </summary>
    public class NotificationDistributionJob : BackgroundJob<NotificationDistributionJobArgs>, ITransientDependency
    {
        private readonly INotificationDistributer _notificationDistributer;
        /// <summary>
        /// 构造函数
        /// </summary>
        public NotificationDistributionJob(INotificationDistributer notificationDistributer)
        {
            _notificationDistributer = notificationDistributer;
        }
        public override void Execute(NotificationDistributionJobArgs args)
        {
            AsyncHelper.RunSync(() => _notificationDistributer.DistributeAsync(args.NotificationId));
        }
    }
}
