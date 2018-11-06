using Abp.EntityFramework.EntityFramework;
using Abp.Zero.Zero;
using AbpFramework.Modules;
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
            base.PreInitialize();
        }
        public override void Initialize()
        {
            base.Initialize();
        }
    }
}
