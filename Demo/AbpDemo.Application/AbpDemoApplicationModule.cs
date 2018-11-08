using Abp.AutoMapper.AutoMapper;
using AbpFramework.Modules;
using System.Reflection;
namespace AbpDemo.Application
{
    [DependsOn(typeof(AbpAutoMapperModule))]
    public class AbpDemoApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //base.PreInitialize();
        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
