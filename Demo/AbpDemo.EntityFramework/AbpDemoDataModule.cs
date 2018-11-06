using Abp.Zero.EntityFramework;
using AbpDemo.EntityFramework.EntityFramework;
using AbpFramework.Modules;
using System.Data.Entity;
using System.Reflection;

namespace AbpDemo.EntityFramework
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule))]
    public class AbpDemoDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<AbpDemoDbContext>());
            Configuration.DefaultNameOrConnectionString = "Default";
        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
        public override void PostInitialize()
        {
            base.PostInitialize();
        }
    }
}
