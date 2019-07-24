using AbpFramework.Dependency;
namespace AbpFramework.Authorization
{
    /// <summary>
    /// 为应用程序定义权限的接口。
    /// 实现它以定义模块的权限.
    /// </summary>
    public abstract class AuthorizationProvider: ITransientDependency
    {
        public abstract void SetPermissions(IPermissionDefinitionContext context);
    }
}
