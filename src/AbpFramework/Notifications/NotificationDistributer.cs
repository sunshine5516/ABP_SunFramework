using AbpFramework.Collections.Extensions;
using AbpFramework.Configuration;
using AbpFramework.Domain.Services;
using AbpFramework.Domain.Uow;
using AbpFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace AbpFramework.Notifications
{
    /// <summary>
    /// 向用户分发通知接口
    /// </summary>
    public class NotificationDistributer : DomainService, INotificationDistributer
    {
        #region 声明实例
        public IRealTimeNotifier RealTimeNotifier { get; set; }
        private readonly INotificationDefinitionManager _notificationDefinitionManager;
        private readonly INotificationStore _notificationStore;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IGuidGenerator _guidGenerator;
        #endregion
        #region 构造函数
        public NotificationDistributer(
          INotificationDefinitionManager notificationDefinitionManager,
          INotificationStore notificationStore,
          IUnitOfWorkManager unitOfWorkManager,
          IGuidGenerator guidGenerator)
        {
            _notificationDefinitionManager = notificationDefinitionManager;
            _notificationStore = notificationStore;
            _unitOfWorkManager = unitOfWorkManager;
            _guidGenerator = guidGenerator;

            RealTimeNotifier = NullRealTimeNotifier.Instance;
        }
        #endregion
        #region 方法
        public async Task DistributeAsync(Guid notificationId)
        {
            var notificationInfo = await _notificationStore.GetNotificationOrNullAsync(notificationId);
            //var notificationInfo = _notificationStore.GetNotificationOrNullAsync(notificationId);
            if (notificationInfo == null)
            {
                Logger.Warn("NotificationDistributionJob can not continue since could not found notification by id: " + notificationId);
                return;
            }
            var users = await GetUsers(notificationInfo);
            var userNotifications=await SaveUserNotifications(users, notificationInfo);
            //var userNotifications = SaveUserNotifications(users, notificationInfo);
            //await _notificationStore.DeleteNotificationAsync(notificationInfo);
            await _notificationStore.DeleteNotificationAsync(notificationInfo);
            try
            {
                await RealTimeNotifier.SendNotificationsAsync(userNotifications.ToArray());
            }
            catch (Exception ex)
            {
                Logger.Warn(ex.ToString(), ex);
            }
        }
        private static int?[] GetTenantIds(NotificationInfo notificationInfo)
        {
            if(notificationInfo.TenantIds.IsNullOrEmpty())
            {
                return null;
            }
            return notificationInfo.TenantIds
                .Split(',')
                .Select(tenantIdAsStr => tenantIdAsStr == "null" ? (int?)null : (int?)tenantIdAsStr.To<int>())
                .ToArray();
        }
        [UnitOfWork]
        protected virtual async Task<UserIdentifier[]> GetUsers(NotificationInfo notificationInfo)
        {
            List<UserIdentifier> userIds;
            if(!notificationInfo.UserIds.IsNullOrEmpty())
            {
                userIds = notificationInfo
                    .UserIds
                    .Split(',')
                    .Select(uidAsStr => UserIdentifier.Parse(uidAsStr))
                    .Where(uid => SettingManager.GetSettingValueForUser<bool>(NotificationSettingNames.ReceiveNotifications, uid.TenantId, uid.UserId))
                    .ToList();
            }
            else
            {
                ///获取订阅的用户
                var tenantIds = GetTenantIds(notificationInfo);
                List<NotificationSubscriptionInfo> subscriptions;
                if(tenantIds.IsNullOrEmpty()||
                    (tenantIds.Length==1&&tenantIds[0] == NotificationInfo.AllTenantIds.To<int>()))
                {
                    subscriptions = await _notificationStore.GetSubscriptionsAsync(
                        notificationInfo.NotificationName,
                        notificationInfo.EntityTypeName,
                        notificationInfo.EntityId);
                }
                else
                {
                    //获取特定租户中的所有订阅用户
                    subscriptions = await _notificationStore.GetSubscriptionsAsync(
                        tenantIds,
                        notificationInfo.NotificationName,
                        notificationInfo.EntityTypeName,
                        notificationInfo.EntityId
                        );
                }
                ///删除无效订阅
                var invalidSubscriptions = new Dictionary<Guid, NotificationSubscriptionInfo>();
                foreach(var subscription in subscriptions)
                {
                    using (CurrentUnitOfWork.SetTenantId(subscription.TenantId))
                    {
                        if(!await _notificationDefinitionManager.IsAvailableAsync(notificationInfo.NotificationName,
                            new UserIdentifier(subscription.TenantId,subscription.UserId)) ||
                            !SettingManager.GetSettingValueForUser<bool>(NotificationSettingNames.ReceiveNotifications, 
                            subscription.TenantId, subscription.UserId))
                        {
                            invalidSubscriptions[subscription.Id] = subscription;
                        }
                    }
                }
                subscriptions.RemoveAll(s => invalidSubscriptions.ContainsKey(s.Id));
                userIds = subscriptions.Select(s => new UserIdentifier(s.TenantId, s.UserId)).ToList();
            }
            if(!notificationInfo.ExcludedUserIds.IsNullOrEmpty())
            {
                var excludedUserIds = notificationInfo.
                    ExcludedUserIds.Split(',').
                    Select(uidAsStr => UserIdentifier.Parse(uidAsStr)).ToList();
                userIds.RemoveAll(uid => excludedUserIds.Any(euid => euid.Equals(uid)));
            }
            return userIds.ToArray();
        }
        [UnitOfWork]
        protected virtual async Task<List<UserNotification>> SaveUserNotifications(UserIdentifier[] users, NotificationInfo notificationInfo)
        {
            var userNotifications = new List<UserNotification>();
            var tenantGroups = users.GroupBy(use => use.TenantId);
            foreach(var tenantGroup in tenantGroups)
            {
                using (_unitOfWorkManager.Current.SetTenantId(tenantGroup.Key))
                {
                    var tenantNotificationInfo = new TenantNotificationInfo(_guidGenerator.Create(), tenantGroup.Key, notificationInfo);
                    await _notificationStore.InsertTenantNotificationAsync(tenantNotificationInfo);
                    //await _unitOfWorkManager.Current.SaveChangesAsync();
                    await CurrentUnitOfWork.SaveChangesAsync();
                    //_unitOfWorkManager.Current.SaveChanges();
                    var tenantNotification = tenantNotificationInfo.ToTenantNotification();
                    foreach(var user in tenantGroup)
                    {
                        var userNotification = new UserNotificationInfo(_guidGenerator.Create())
                        {
                            TenantId = tenantGroup.Key,
                            UserId = user.UserId,
                            TenantNotificationId = tenantNotificationInfo.Id
                        };
                        await _notificationStore.InsertUserNotificationAsync(userNotification);
                        userNotifications.Add(userNotification.ToUserNotification(tenantNotification));
                    }
                    await CurrentUnitOfWork.SaveChangesAsync();
                    //CurrentUnitOfWork.SaveChanges();
                }
            }
            return userNotifications;
        }
        #endregion

    }
}
