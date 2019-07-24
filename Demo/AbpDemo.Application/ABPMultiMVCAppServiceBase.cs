using AbpDemo.Core;
using AbpDemo.Core.Authorization.Users;
using AbpDemo.Core.MultiTenancy;
using AbpFramework.Application.Services;
using AbpFramework.Runtime.Session;
using System;
using System.Threading.Tasks;
namespace AbpDemo.Application
{
    public abstract class ABPMultiMVCAppServiceBase:ApplicationService
    {
        #region 声明实例
        public TenantManager TenantManager { get; set; }
        public UserManager UserManager { get; set; }
        public string LocalizationSourceName { get; set; }
        #endregion
        #region 构造函数
        protected ABPMultiMVCAppServiceBase()
        {
            LocalizationSourceName = AbpDemoConsts.LocalizationSourceName;            
        }
        #endregion
        #region 方法
        protected virtual Task<User> GetCurrentUserAsync()
        {
            //UserManager.FindByNameAsync("12");
            //var user = UserManager.FindByIdAsync(AbpSession.GetUserId());
            Task<User> user = UserManager.FindByIdAsync(AbpSession.GetUserId());
            if (user == null)
            {
                throw new ApplicationException("There is no current user!");
            }
            return user;
        }
        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
        }
        #endregion
    }
}
