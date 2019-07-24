using System.Threading.Tasks;
namespace AbpFramework.Authorization
{
    public interface IPermissionChecker
    {
        /// <summary>
        /// 检查当前用户是否被授予权限。
        /// </summary>
        /// <param name="permissionName"></param>
        /// <returns></returns>
        Task<bool> IsGrantedAsync(string permissionName);
        /// <summary>
        /// 检查当前用户是否被授予权限。
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        Task<bool> IsGrantedAsync(UserIdentifier user, string permissionName);
    }
}
