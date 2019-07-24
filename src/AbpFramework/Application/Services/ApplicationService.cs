using AbpFramework.Authorization;
using AbpFramework.Runtime.Session;

namespace AbpFramework.Application.Services
{
    /// <summary>
    /// 所有其他appservice的基类
    /// 其封装了对AbpSession, Permission和Feature这些模块的功能调用.
    /// </summary>
    public abstract class ApplicationService : AbpServiceBase, IApplicationService
    {
        #region 声明实例
        public static string[] CommonPostfixes = { "AppService", "ApplicationService" };
        public IAbpSession AbpSession { get; set; }
        /// <summary>
        /// 权限管理
        /// </summary>
        public IPermissionManager PermissionManager { protected get; set; }
        public IPermissionChecker PermissionChecker { protected get; set; }
        #endregion
        #region 构造函数
        public ApplicationService()
        {
            AbpSession = NullAbpSession.Instance;
            PermissionChecker= NullPermissionChecker.Instance;
        }
        #endregion
        #region 方法

        #endregion
    }
}
