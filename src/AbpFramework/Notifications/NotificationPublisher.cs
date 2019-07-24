using System;
using System.Linq;
using System.Threading.Tasks;
using AbpFramework.BackgroundJobs;
using AbpFramework.Collections.Extensions;
using AbpFramework.Dependency;
using AbpFramework.Domain.Entities;
using AbpFramework.Domain.Uow;
using AbpFramework.Extensions;
using AbpFramework.Json;
using AbpFramework.Runtime.Session;

namespace AbpFramework.Notifications
{
    public class NotificationPublisher : AbpServiceBase, INotificationPublisher, ITransientDependency
    {
        #region 声明实例
        public const int MaxUserCountToDirectlyDistributeANotification = 5;//大于5，加到消息队列中
        public static int[] AllTenants
        {
            get
            {
                return new[] { NotificationInfo.AllTenantIds.To<int>() };
            }
        }
        public IAbpSession AbpSession { get; set; }
        private readonly INotificationStore _store;
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly INotificationDistributer _notificationDistributer;
        private readonly IGuidGenerator _guidGenerator;
        #endregion
        #region 构造函数
        public NotificationPublisher(
            INotificationStore store,
            IBackgroundJobManager backgroundJobManager,
            INotificationDistributer notificationDistributer,
            IGuidGenerator guidGenerator)
        {
            _store = store;
            _backgroundJobManager = backgroundJobManager;
            _notificationDistributer = notificationDistributer;
            _guidGenerator = guidGenerator;
            AbpSession = NullAbpSession.Instance;
        }
        #endregion
        #region 方法
        [UnitOfWork]
        public virtual async Task PublishAsync(
           string notificationName,
           NotificationData data = null,
           EntityIdentifier entityIdentifier = null,
           NotificationSeverity severity = NotificationSeverity.Info,
           UserIdentifier[] userIds = null,
           UserIdentifier[] excludedUserIds = null,
           int?[] tenantIds = null)
        {
            if (notificationName.IsNullOrEmpty())
            {
                throw new ArgumentException("NotificationName can not be null or whitespace!", "notificationName");
            }

            if (!tenantIds.IsNullOrEmpty() && !userIds.IsNullOrEmpty())
            {
                throw new ArgumentException("tenantIds can be set only if userIds is not set!", "tenantIds");
            }
            if(tenantIds.IsNullOrEmpty()&&userIds.IsNullOrEmpty())
            {
                tenantIds = new[] { AbpSession.TenantId };
            }
            var notificationInfo = new NotificationInfo(_guidGenerator.Create())
            {
                NotificationName = notificationName,
                EntityTypeName = entityIdentifier == null ? null : entityIdentifier.Type.FullName,
                EntityTypeAssemblyQualifiedName = entityIdentifier == null ? null : entityIdentifier.Type.AssemblyQualifiedName,
                EntityId = entityIdentifier == null ? null : entityIdentifier.Id.ToJsonString(),
                Severity = severity,
                UserIds = userIds.IsNullOrEmpty() ? null : userIds.Select(uid => uid.ToUserIdentifierString()).JoinAsString(","),
                ExcludedUserIds = excludedUserIds.IsNullOrEmpty() ? null : excludedUserIds.Select(uid => uid.ToUserIdentifierString()).JoinAsString(","),
                TenantIds = tenantIds.IsNullOrEmpty() ? null : tenantIds.JoinAsString(","),
                Data = data == null ? null : data.ToJsonString(),
                DataTypeName = data == null ? null : data.GetType().AssemblyQualifiedName
            };
            await _store.InsertNotificationAsync(notificationInfo);
            await CurrentUnitOfWork.SaveChangesAsync();
            //CurrentUnitOfWork.SaveChangesAsync();
            //CurrentUnitOfWork.SaveChanges();
            if (userIds!=null&&userIds.Length<=MaxUserCountToDirectlyDistributeANotification)
            {
                await _notificationDistributer.DistributeAsync(notificationInfo.Id);
            }
            else
            {
                await _backgroundJobManager.EnqueueAsync<NotificationDistributionJob, NotificationDistributionJobArgs>
                    (new NotificationDistributionJobArgs(notificationInfo.Id));
            }
        }
        #endregion

    }
}
