using AbpFramework.Dependency;
namespace AbpFramework.Authorization
{
    /// <summary>
    /// 权限依赖上下文
    /// </summary>
    public interface IPermissionDependencyContext
    {
        /// <summary>
        /// 需要权限的用户，可以为空；
        /// </summary>
        UserIdentifier User { get; }
        /// <summary>
        /// <see cref="IIocResolver"/>实例.
        /// </summary>
        /// <value>
        /// The ioc resolver.
        /// </value>
        IIocResolver IocResolver { get; }
        /// <summary>
        /// Gets the <see cref="IFeatureChecker"/>.
        /// </summary>
        /// <value>
        /// The feature checker.
        /// </value>
        IPermissionChecker PermissionChecker { get; }
    }
}
