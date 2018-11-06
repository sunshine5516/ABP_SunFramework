using Abp.Web.Mvc.Web.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AbpDemo.Web.Controllers
{
    public abstract class ABPFrameworkDemoControllerBase: AbpController
    {
        protected ABPFrameworkDemoControllerBase()
        {
            //LocalizationSourceName = AbpDemoConsts.LocalizationSourceName;
        }
    }
}