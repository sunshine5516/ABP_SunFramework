using AbpFramework.Authorization;
using AbpFramework.Collections.Extensions;
using AbpFramework.Dependency;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
namespace AbpFramework.Notifications
{
    /// <summary>
    /// <see cref="NotificationDefinition"/>管理，单例对象.
    /// </summary>
    internal class NotificationDefinitionManager : INotificationDefinitionManager, ISingletonDependency
    {
        #region 声明实例
        private readonly INotificationConfiguration _configuration;
        private readonly IocManager _iocManager;
        private readonly IDictionary<string, NotificationDefinition> _notificationDefinitions;
        #endregion
        #region 构造函数
        public NotificationDefinitionManager(
           IocManager iocManager,
           INotificationConfiguration configuration)
        {
            _configuration = configuration;
            _iocManager = iocManager;

            _notificationDefinitions = new Dictionary<string, NotificationDefinition>();
        }
        #endregion
        #region 方法
        public void Initialize()
        {
            var context = new NotificationDefinitionContext(this);
            foreach(var providerType in _configuration.Providers)
            {
                using (var provider = _iocManager.ResolveAsDisposable<NotificationProvider>(providerType))
                {
                    provider.Object.SetNotifications(context);
                }
            }
        }

        public void Add(NotificationDefinition notificationDefinition)
        {
            if(_notificationDefinitions.ContainsKey(notificationDefinition.Name))
            {
                throw new AbpException("There is already a notification definition with given name: " 
                    + notificationDefinition.Name + ". Notification names must be unique!");
            }
            _notificationDefinitions[notificationDefinition.Name] = notificationDefinition;
        }

        public NotificationDefinition Get(string name)
        {
            var definition = GetOrNull(name);
            if(definition==null)
            {
                throw new AbpException("There is no notification definition with given name: " + name);
            }
            return definition;
        }

        public IReadOnlyList<NotificationDefinition> GetAll()
        {
            return _notificationDefinitions.Values.ToImmutableList();
        }

        public async Task<IReadOnlyList<NotificationDefinition>> GetAllAvailableAsync(UserIdentifier user)
        {
            var availableDefinitions = new List<NotificationDefinition>();
            using (var permissionDependencyContext = _iocManager.ResolveAsDisposable<PermissionDependencyContext>())
            {
                permissionDependencyContext.Object.User = user;
                foreach(var notificationDefinition in GetAll())
                {
                    if(notificationDefinition.PermissionDependency!=null&&
                        !await notificationDefinition.PermissionDependency.IsSatisfiedAsync(permissionDependencyContext.Object))
                    {
                        continue;
                    }
                    if(user.TenantId.HasValue)
                    {
                        continue;
                    }
                    availableDefinitions.Add(notificationDefinition);
                }
            }
            return availableDefinitions.ToImmutableList();
        }

        public NotificationDefinition GetOrNull(string name)
        {
            return _notificationDefinitions.GetOrDefault(name);
        }

        public async Task<bool> IsAvailableAsync(string name, UserIdentifier user)
        {
            var notificationDefinition = GetOrNull(name);
            if(notificationDefinition==null)
            {
                return true;
            }
            if(notificationDefinition.PermissionDependency!=null)
            {
                using (var permissionDependencyContext = _iocManager.ResolveAsDisposable<PermissionDependencyContext>())
                {
                    permissionDependencyContext.Object.User = user;
                    if(!await notificationDefinition.PermissionDependency.IsSatisfiedAsync(permissionDependencyContext.Object))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion
    }
}
