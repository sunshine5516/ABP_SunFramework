using AbpFramework;
using AbpFramework.Dependency;
using AbpFramework.Modules;
using AbpFramework.Net.Mail;
using AbpFramework.Configuration.Startup;
using AbpFramework.Reflection.Extensions;
namespace Abp.MailKit
{
    [DependsOn(typeof(AbpKernelModule))]
    public class AbpMailKitModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.ReplaceService<IEmailSender, MailKitEmailSender>(DependencyLifeStyle.Transient);
        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpMailKitModule).GetAssembly());
        }
    }
}
