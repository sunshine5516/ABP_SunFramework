using System.Threading.Tasks;

namespace AbpFramework.Authorization
{
    /// <summary>
    /// <see cref="IPermissionDependency"/>最简单的实现.
    /// 检查是否有一个或多个权限.
    /// </summary>
    public class SimplePermissionDependency
    {
        /// <summary>
        /// 待检测的权限集合
        /// </summary>
        public string[] Permissions { get; set; }
        public bool RequiresAll { get; set; }
        #region 构造函数
        public SimplePermissionDependency(params string[] permissions)
        {
            Permissions = permissions;
        }
        public SimplePermissionDependency(bool requiresAll, params string[] permissions)
            : this(permissions)
        {
            RequiresAll = requiresAll;
        }
        public Task<bool> IsSatisfiedAsync(IPermissionDependencyContext context)
        {
            return context.User != null
                ? context.PermissionChecker.IsGrantedAsync(context.User, RequiresAll, Permissions)
                : context.PermissionChecker.IsGrantedAsync(RequiresAll, Permissions);
        }
        #endregion
    }
}
