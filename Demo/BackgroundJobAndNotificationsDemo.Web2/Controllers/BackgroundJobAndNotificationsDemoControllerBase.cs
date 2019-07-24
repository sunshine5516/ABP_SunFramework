using Abp.Web.Mvc.Web.Mvc.Controllers;
using AbpDemo.Core;
using AbpFramework.UI;
using Microsoft.AspNet.Identity;
namespace BackgroundJobAndNotificationsDemo.Web.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
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
           // identityResult.CheckErrors(LocalizationManager);
        }
    }
}