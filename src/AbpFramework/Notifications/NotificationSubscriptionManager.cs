using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbpFramework.Dependency;
using AbpFramework.Domain.Entities;
using AbpFramework.Json;
namespace AbpFramework.Notifications
{
    public class NotificationSubscriptionManager : INotificationSubscriptionManager, ITransientDependency
    {
        #region 声明实例
        private readonly INotificationStore _store;
        private readonly INotificationDefinitionManager _notificationDefinitionManager;
        private readonly IGuidGenerator _guidGenerator;
        #endregion
        #region 构造函数
        public NotificationSubscriptionManager(
          INotificationStore store,
          INotificationDefinitionManager notificationDefinitionManager,
          IGuidGenerator guidGenerator)
        {
            _store = store;
            _notificationDefinitionManager = notificationDefinitionManager;
            _guidGenerator = guidGenerator;
        }
        #endregion
        #region 方法
        public async Task<List<NotificationSubscription>> GetSubscribedNotificationsAsync(UserIdentifier user)
        {
            var notificationSubscriptionInfos = await _store.GetSubscriptionsAsync(user);
            return notificationSubscriptionInfos.Select(nsi => nsi.ToNotificationSubscription()).ToList();
        }

        public async Task<List<NotificationSubscription>> GetSubscriptionsAsync
            (string notificationName, EntityIdentifier entityIdentifier = null)
        {
            var notificationSubscriptionInfos = await _store.GetSubscriptionsAsync(
                notificationName,
                entityIdentifier == null ? null : entityIdentifier.Type.FullName,
                entityIdentifier == null ? null : entityIdentifier.Id.ToJsonString()
                );
            return notificationSubscriptionInfos.Select
                (nsi => nsi.ToNotificationSubscription())
                .ToList();
        }

        public async Task<List<NotificationSubscription>> GetSubscriptionsAsync
            (int? tenantId, string notificationName, EntityIdentifier entityIdentifier = null)
        {
            var notificationSubscriptionInfos = await _store.GetSubscriptionsAsync(
                new[] { tenantId },
                notificationName,
                entityIdentifier == null ? null : entityIdentifier.Type.FullName,
                entityIdentifier == null ? null : entityIdentifier.Id.ToJsonString()
                );
            return notificationSubscriptionInfos.Select(nsi =>
            nsi.ToNotificationSubscription()).ToList();
        }

        public Task<bool> IsSubscribedAsync(
            UserIdentifier user,
            string notificationName, 
            EntityIdentifier entityIdentifier = null)
        {
            return _store.IsSubscribedAsync(
                user,notificationName,
                entityIdentifier == null ? null : entityIdentifier.Type.FullName,
                entityIdentifier == null ? null : entityIdentifier.Id.ToJsonString()
                );
        }

        public async Task SubscribeAsync(UserIdentifier user, string notificationName, EntityIdentifier entityIdentifier = null)
        {
            if (await IsSubscribedAsync(user, notificationName, entityIdentifier))
            {
                return;
            }
            await _store.InsertSubscriptionAsync(new NotificationSubscriptionInfo(
                    _guidGenerator.Create(),
                    user.TenantId,
                    user.UserId,
                    notificationName,
                    entityIdentifier
                    ));
        }
        /// <summary>
        /// 订阅给定用户的所有可用通知
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task SubscribeToAllAvailableNotificationsAsync(UserIdentifier user)
        {
            var notificationDefinitions = (await _notificationDefinitionManager.GetAllAvailableAsync(user))
                .Where(nd => nd.EntityType == null).ToList();
            foreach (var notificationDefinition in notificationDefinitions)
            {
                await SubscribeAsync(user, notificationDefinition.Name);
            }
        }

        public async Task UnsubscribeAsync(UserIdentifier user, string notificationName, EntityIdentifier entityIdentifier = null)
        {
            await _store.DeleteSubscriptionAsync(
                user,
                notificationName,
                entityIdentifier == null ? null : entityIdentifier.Type.FullName,
                entityIdentifier == null ? null : entityIdentifier.Id.ToJsonString()
                );
        }
        #endregion

    }
}
