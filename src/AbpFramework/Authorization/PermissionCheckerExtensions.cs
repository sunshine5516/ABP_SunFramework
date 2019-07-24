using AbpFramework.Collections.Extensions;
using AbpFramework.Threading;
using System.Threading.Tasks;

namespace AbpFramework.Authorization
{
    public static class PermissionCheckerExtensions
    {
        /// <summary>
        /// 授权当前用户获得给定的权限,如果未授权，抛出<see cref="AbpAuthorizationException"/>
        /// </summary>
        /// <param name="permissionChecker"></param>
        /// <param name="permissionNames">权限名称</param>
        public static void Authorize(this IPermissionChecker permissionChecker,params string[] permissionNames)
        {
            Authorize(permissionChecker, false, permissionNames);
        }
        public static bool IsGranted(this IPermissionChecker permissionChecker, string permissionName)
        {
            return AsyncHelper.RunSync(() => permissionChecker.IsGrantedAsync(permissionName));
        }
        public static void Authorize(this IPermissionChecker permissionChecker,bool requireAll,params string[] permissionNames)
        {
            AsyncHelper.RunSync(() => AuthorizeAsync(permissionChecker, requireAll, permissionNames));
        }
        public static async Task AuthorizeAsync(this IPermissionChecker permissionChecker,
            bool requireAll, params string[] permissionNames)
        {
            if(await IsGrantedAsync(permissionChecker,requireAll,permissionNames))
            {
                return;
            }
            //var 
        }
        /// <summary>
        /// 检查当前用户是否被授予给定权限
        /// </summary>
        /// <param name="permissionChecker"></param>
        /// <param name="requiresAll"></param>
        /// <param name="permissionNames"></param>
        /// <returns></returns>
        public static async Task<bool> IsGrantedAsync(this IPermissionChecker permissionChecker,
            bool requiresAll,params string[] permissionNames)
        {
            if (permissionNames.IsNullOrEmpty())
            {
                return true;
            }
            if (requiresAll)
            {
                foreach(var permissionName in permissionNames)
                {
                    if(!(await permissionChecker.IsGrantedAsync(permissionName)))
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                foreach (var permissionName in permissionNames)
                {
                    if (await permissionChecker.IsGrantedAsync(permissionName))
                    {
                        return true;
                    }
                }

                return false;
            }
        }
        public static async Task<bool> IsGrantedAsync(this IPermissionChecker permissionChecker,
            UserIdentifier user,bool requiresAll,params string[] permissionNames)
        {
            if(permissionNames.IsNullOrEmpty())
            {
                return true;
            }
            if(requiresAll)
            {
                foreach(var permissionName in permissionNames)
                {
                    if(!(await permissionChecker.IsGrantedAsync(user,permissionName)))
                    {
                        return false;
                    }
                }
                return false;
            }
            else
            {
                foreach(var permissionName in permissionNames)
                {
                    if(await permissionChecker.IsGrantedAsync(user,permissionName))
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
