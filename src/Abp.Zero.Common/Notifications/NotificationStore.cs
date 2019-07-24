using AbpFramework;
using AbpFramework.Dependency;
using AbpFramework.Domain.Repositories;
using AbpFramework.Domain.Uow;
using AbpFramework.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Abp.Zero.Common.Notifications
{
    public class NotificationStore : INotificationStore, ITransientDependency
    {
        #region 声明实例
        private readonly IRepository<NotificationInfo, Guid> _notificationRepository;
        private readonly IRepository<TenantNotificationInfo, Guid> _tenantNotificationRepository;
        private readonly IRepository<UserNotificationInfo, Guid> _userNotificationRepository;
        private readonly IRepository<NotificationSubscriptionInfo, Guid> _notificationSubscriptionRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        #endregion
        #region 构造函数
        public NotificationStore(
            IRepository<NotificationInfo, Guid> notificationRepository,
            IRepository<TenantNotificationInfo, Guid> tenantNotificationRepository,
            IRepository<UserNotificationInfo, Guid> userNotificationRepository,
            IRepository<NotificationSubscriptionInfo, Guid> notificationSubscriptionRepository,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _notificationRepository = notificationRepository;
            _tenantNotificationRepository = tenantNotificationRepository;
            _userNotificationRepository = userNotificationRepository;
            _notificationSubscriptionRepository = notificationSubscriptionRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }
        #endregion
        [UnitOfWork]
        public virtual async Task DeleteAllUserNotificationsAsync(UserIdentifier user)
        {
            using (_unitOfWorkManager.Current.SetTenantId(user.TenantId))
            {
                await _userNotificationRepository.DeleteAsync(un => un.UserId == user.UserId);
                await _unitOfWorkManager.Current.SaveChangesAsync();
            }
        }

        public virtual Task DeleteNotificationAsync(NotificationInfo notification)
        {
            return _notificationRepository.DeleteAsync(notification);
        }
        [UnitOfWork]
        public virtual async Task DeleteSubscriptionAsync(UserIdentifier user, string notificationName, string entityTypeName, string entityId)
        {
            using (_unitOfWorkManager.Current.SetTenantId(user.TenantId))
            {
                await _notificationSubscriptionRepository.DeleteAsync(s =>
                    s.UserId == user.UserId &&
                    s.NotificationName == notificationName &&
                    s.EntityTypeName == entityTypeName &&
                    s.EntityId == entityId
                    );
                await _unitOfWorkManager.Current.SaveChangesAsync();
            }
        }
        [UnitOfWork]
        public virtual async Task DeleteUserNotificationAsync(int? tenantId, Guid userNotificationId)
        {
            using (_unitOfWorkManager.Current.SetTenantId(tenantId))
            {
                await _userNotificationRepository.DeleteAsync(userNotificationId);
                await _unitOfWorkManager.Current.SaveChangesAsync();
            }
        }
        [UnitOfWork]
        public virtual async Task<NotificationInfo> GetNotificationOrNullAsync(Guid notificationId)
        {
            using (_unitOfWorkManager.Current.SetTenantId(null))
            {
                return await _notificationRepository.FirstOrDefaultAsync(notificationId);
            }
        }
        [UnitOfWork]
        public virtual Task<List<NotificationSubscriptionInfo>> GetSubscriptionsAsync(string notificationName, string entityTypeName, string entityId)
        {
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                return _notificationSubscriptionRepository.GetAllListAsync(s =>
                s.NotificationName == notificationName &&
                s.EntityTypeName == entityTypeName &&
                s.EntityId == entityId);
            }
        }
        [UnitOfWork]
        public virtual async Task<List<NotificationSubscriptionInfo>> GetSubscriptionsAsync(int?[] tenantIds,
            string notificationName, string entityTypeName, string entityId)
        {
            var subscriptions = new List<NotificationSubscriptionInfo>();
            foreach(var tenantId in tenantIds)
            {
                subscriptions.AddRange(await GetSubscriptionsAsync(tenantId, notificationName, entityTypeName, entityId));
            }
            return subscriptions;
        }
        [UnitOfWork]
        protected virtual async Task<List<NotificationSubscriptionInfo>> GetSubscriptionsAsync(int? tenantId, string notificationName, string entityTypeName, string entityId)
        {
            using (_unitOfWorkManager.Current.SetTenantId(tenantId))
            {
                return await _notificationSubscriptionRepository.GetAllListAsync(s =>
                s.NotificationName == notificationName &&
                s.EntityTypeName == entityTypeName &&
                s.EntityId == entityId);
            }
        }
        [UnitOfWork]
        public virtual async Task<List<NotificationSubscriptionInfo>> GetSubscriptionsAsync(UserIdentifier user)
        {
            using (_unitOfWorkManager.Current.SetTenantId(user.TenantId))
            {
                return await _notificationSubscriptionRepository.GetAllListAsync(s => s.UserId == user.UserId);
            }
        }
        [UnitOfWork]
        public virtual async Task<int> GetUserNotificationCountAsync(UserIdentifier user, UserNotificationState? state = null)
        {
            using (_unitOfWorkManager.Current.SetTenantId(user.TenantId))
            {
                return await _userNotificationRepository.CountAsync(un => un.UserId == user.UserId &&
                state == null || un.State == state.Value);
            }
        }
        [UnitOfWork]
        public virtual Task<List<UserNotificationInfoWithNotificationInfo>> GetUserNotificationsWithNotificationsAsync(UserIdentifier user,
            UserNotificationState? state = null, int skipCount = 0, int maxResultCount = int.MaxValue)
        {
            using (_unitOfWorkManager.Current.SetTenantId(user.TenantId))
            {
                var query = from userNotificationInfo in _userNotificationRepository.GetAll()
                            join tenantNotificationInfo in _tenantNotificationRepository.GetAll()
                            on userNotificationInfo.TenantNotificationId equals tenantNotificationInfo.Id
                            where userNotificationInfo.UserId == user.UserId && (state == null || userNotificationInfo.State == state.Value)
                            orderby tenantNotificationInfo.CreationTime descending
                            select new { userNotificationInfo, tenantNotificationInfo = tenantNotificationInfo };
                var list = query.ToList();
                return Task.FromResult(list.Select(
                    a => new UserNotificationInfoWithNotificationInfo(a.userNotificationInfo, a.tenantNotificationInfo)
                    ).ToList());
            }
        }
        [UnitOfWork]
        public Task<UserNotificationInfoWithNotificationInfo> GetUserNotificationWithNotificationOrNullAsync
            (int? tenantId, Guid userNotificationId)
        {
            using (_unitOfWorkManager.Current.SetTenantId(tenantId))
            {
                var query = from userNotificationInfo in _userNotificationRepository.GetAll()
                            join tenantNotificationInfo in _tenantNotificationRepository.GetAll() 
                            on userNotificationInfo.TenantNotificationId equals tenantNotificationInfo.Id
                            where userNotificationInfo.Id == userNotificationId
                            select new { userNotificationInfo, tenantNotificationInfo = tenantNotificationInfo };
                var item = query.FirstOrDefault();
                if (item == null)
                {
                    return Task.FromResult((UserNotificationInfoWithNotificationInfo)null);
                }

                return Task.FromResult(new UserNotificationInfoWithNotificationInfo(item.userNotificationInfo, item.tenantNotificationInfo));
            }
        }
        [UnitOfWork]
        public virtual async Task InsertNotificationAsync(NotificationInfo notification)
        {
            using (_unitOfWorkManager.Current.SetTenantId(null))
            {
                await _notificationRepository.InsertAsync(notification);
                await _unitOfWorkManager.Current.SaveChangesAsync();
                //_unitOfWorkManager.Current.SaveChanges();
            }
        }
        [UnitOfWork]
        public virtual async Task InsertSubscriptionAsync(NotificationSubscriptionInfo subscription)
        {
            using (_unitOfWorkManager.Current.SetTenantId(subscription.TenantId))
            {
                await _notificationSubscriptionRepository.InsertAsync(subscription);
                await _unitOfWorkManager.Current.SaveChangesAsync();
            }
        }
        [UnitOfWork]
        public virtual async Task InsertTenantNotificationAsync(TenantNotificationInfo tenantNotificationInfo)
        {
            using (_unitOfWorkManager.Current.SetTenantId(tenantNotificationInfo.TenantId))
            {
                await _tenantNotificationRepository.InsertAsync(tenantNotificationInfo);
            }
        }
        [UnitOfWork]
        public virtual async Task InsertUserNotificationAsync(UserNotificationInfo userNotification)
        {
            using (_unitOfWorkManager.Current.SetTenantId(userNotification.TenantId))
            {
                await _userNotificationRepository.InsertAsync(userNotification);
                await _unitOfWorkManager.Current.SaveChangesAsync();
                //_unitOfWorkManager.Current.SaveChanges();
            }
        }
        [UnitOfWork]
        public virtual async Task<bool> IsSubscribedAsync(UserIdentifier user, string notificationName, string entityTypeName, string entityId)
        {
            using (_unitOfWorkManager.Current.SetTenantId(user.TenantId))
            {
                return await _notificationSubscriptionRepository.CountAsync(s =>
                s.UserId == user.UserId &&
                s.NotificationName == notificationName &&
                s.EntityTypeName == entityTypeName &&
                s.EntityId == entityId) > 0;
            }
        }
        [UnitOfWork]
        public virtual async Task UpdateAllUserNotificationStatesAsync(UserIdentifier user, UserNotificationState state)
        {
            using (_unitOfWorkManager.Current.SetTenantId(user.TenantId))
            {
                var userNotifications=await _userNotificationRepository.GetAllListAsync(un => un.UserId == user.UserId);
                foreach(var userNotification in userNotifications)
                {
                    userNotification.State = state;
                }
                await _unitOfWorkManager.Current.SaveChangesAsync();
            }
        }
        [UnitOfWork]
        public virtual async Task UpdateUserNotificationStateAsync(int? tenantId, Guid userNotificationId, UserNotificationState state)
        {
            using (_unitOfWorkManager.Current.SetTenantId(tenantId))
            {
                var userNotification = await _userNotificationRepository.FirstOrDefaultAsync(userNotificationId);
                if (userNotification == null)
                {
                    return;
                }

                userNotification.State = state;
                await _unitOfWorkManager.Current.SaveChangesAsync();
            }
        }
    }
}
