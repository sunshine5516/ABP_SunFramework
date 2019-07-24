using Abp.EntityFramework.EntityFramework;
using Abp.Zero.Zero;
using AbpFramework.Domain.Uow;
using AbpFramework.Modules;
using Castle.MicroKernel.Registration;
using System.Reflection;

namespace Abp.Zero.EntityFramework
{
    /// <summary>
    /// ASP.NET Boilerplate Zero的实体框架集成模块。
    /// </summary>
    [DependsOn(typeof(AbpZeroCoreModule), typeof(AbpEntityFrameworkModule))]
    public class AbpZeroEntityFrameworkModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.ReplaceService(typeof(IConnectionStringResolver), () =>
            {
                //IocManager.IocContainer.Register(
                //    Component.For<IConnectionStringResolver, IDbPerTenantConnectionStringResolver>()
                //        .ImplementedBy<DbPerTenantConnectionStringResolver>()
                //        .LifestyleTransient()
                //    );
            });
        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
