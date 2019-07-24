using System.Security.Claims;
using System.Web;
using AbpFramework.Runtime.Session;
namespace Abp.Web.Web.Session
{
    public class HttpContextPrincipalAccessor: DefaultPrincipalAccessor
    {
        //public override ClaimsPrincipal Principal => HttpContext.Current?.User as 
        //    ClaimsPrincipal??base.Principal;
        public override ClaimsPrincipal Principal => HttpContext.Current?.User as
           ClaimsPrincipal;
    }
}
