using System.Web;
using AbpFramework.Dependency;
namespace Abp.Web.Web.Localization
{
    public class CurrentCultureSetter : ICurrentCultureSetter, ITransientDependency
    {
        public virtual void SetCurrentCulture(HttpContext httpContext)
        {
            //if (IsCultureSpecifiedInGlobalizationConfig())
            //{
            //    return;
            //}
            //var culture= GetCultureFromQueryString(httpContext);
            //if(culture!=null)
            //{
            //    SetCurrentCulture(culture);
            //    return;
            //}
            //culture= GetCultureFromUserSetting();
            //if (culture != null)
            //{
            //    SetCurrentCulture(culture);
            //    return;
            //}
            //throw new System.NotImplementedException();
        }
    }
}
