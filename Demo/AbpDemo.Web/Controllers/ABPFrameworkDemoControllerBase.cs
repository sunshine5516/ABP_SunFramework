using Abp.Web.Mvc.Web.Mvc.Controllers;
using AbpDemo.Core;
using Microsoft.AspNet.Identity;
using System;
namespace AbpDemo.Web.Controllers
{
    public abstract class ABPFrameworkDemoControllerBase: AbpController
    {
        protected ABPFrameworkDemoControllerBase()
        {
            LocalizationSourceName = AbpDemoConsts.LocalizationSourceName;
        }
        protected virtual void CheckModelState()
        {
            if(!ModelState.IsValid)
            {
                throw new Exception(("FormIsNotValidMessage"));
            }
        }
        protected void CheckErrors(IdentityResult identityResult)
        {
            //identityResult.CheckErrors(LocalizationManager);
        }
    }
}