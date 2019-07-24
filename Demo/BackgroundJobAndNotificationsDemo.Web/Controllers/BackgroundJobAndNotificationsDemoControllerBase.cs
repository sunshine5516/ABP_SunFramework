using Abp.Web.Mvc.Web.Mvc.Controllers;
using AbpDemo.Core;
using AbpFramework.UI;
using Microsoft.AspNet.Identity;
namespace BackgroundJobAndNotificationsDemo.Web.Controllers
{
    public abstract class BackgroundJobAndNotificationsDemoControllerBase : AbpController
    {
        protected BackgroundJobAndNotificationsDemoControllerBase()
        {
            LocalizationSourceName = AbpDemoConsts.LocalizationSourceName;
        }
        protected virtual void CheckModelState()
        {
            if (!ModelState.IsValid)
            {
                throw new UserFriendlyException(("FormIsNotValidMessage"));
            }
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            
        }
    }
}