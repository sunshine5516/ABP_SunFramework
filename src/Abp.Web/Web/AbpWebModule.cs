using Abp.Web.Web.Session;
using AbpFramework.Dependency;
using AbpFramework.Modules;
using AbpFramework.Runtime.Session;
using AbpFramework.Configuration.Startup;
using System.Reflection;

namespace Abp.Web.Web
{
    //[DependsOn(typeof(AbpWebCommonModule))]
    public class AbpWebModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.ReplaceService<IPrincipalAccessor, HttpContextPrincipalAccessor>
                (DependencyLifeStyle.Transient);
        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
        //public override void PostInitialize()
        //{
        //    base.PostInitialize();
        //}
    }
}
