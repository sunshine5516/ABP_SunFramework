using AbpFramework.Auditing;
using AbpFramework.Domain.Uow;
using AbpFramework.Extensions;
using AbpFramework.Runtime.Validation;
using System;
using System.Web.Mvc;
namespace Abp.Web.Mvc.Web.Mvc.Controllers
{
    public class AbpAppViewController : AbpController
    {
        [DisableAuditing]
        [DisableValidation]
        [UnitOfWork(IsDisabled = true)]
        public ActionResult Load(string viewUrl)
        {
            if (viewUrl.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(viewUrl));
            }

            return View(viewUrl.EnsureStartsWith('~'));
        }
    }
}
