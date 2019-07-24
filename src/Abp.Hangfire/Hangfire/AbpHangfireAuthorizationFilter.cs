using AbpFramework.Authorization;
using AbpFramework.Dependency;
using AbpFramework.Runtime.Session;
using Castle.Core.Internal;
using Hangfire.Annotations;
using Hangfire.Dashboard;
namespace Abp.Hangfire.Hangfire
{
    public class AbpHangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public IIocResolver IocResolver { get; set; }
        private readonly string _requiredPermissionName;
        public AbpHangfireAuthorizationFilter(string requiredPermissionName = null)
        {
            _requiredPermissionName = requiredPermissionName;

            IocResolver = IocManager.Instance;
        }
        public bool Authorize([NotNull] DashboardContext context)
        {
            if(!IsLoggedIn())
            {
                return false;
            }
            if (!_requiredPermissionName.IsNullOrEmpty() && !IsPermissionGranted(_requiredPermissionName))
            {
                return false;
            }

            return true;
        }

        private bool IsPermissionGranted(string requiredPermissionName)
        {
            using (var permissionChecker = IocResolver.ResolveAsDisposable<IPermissionChecker>())
            {
                return permissionChecker.Object.IsGranted(requiredPermissionName);
            }
        }

        private bool IsLoggedIn()
        {
            using (var abpSession = IocResolver.ResolveAsDisposable<IAbpSession>())
            {
                return abpSession.Object.UserId.HasValue;
            }
        }
    }
}
