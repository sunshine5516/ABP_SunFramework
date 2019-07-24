using AbpFramework.BackgroundJobs;
using AbpFramework.Configuration.Startup;
using System;
namespace Abp.Hangfire.Hangfire.Configuration
{
    public static class AbpHangfireConfigurationExtensions
    {
        public static IAbpHangfireConfiguration AbpHangfire(this IModuleConfigurations configurations)
        {
            return configurations.AbpConfiguration.Get<IAbpHangfireConfiguration>();
        }
        /// <summary>
        /// 启用hangfire，替代默认后台作业管理器
        /// </summary>
        /// <param name="backgroundJobConfiguration"></param>
        /// <param name="configureAction"></param>
        public static void UseHangfire(this IBackgroundJobConfiguration backgroundJobConfiguration, 
            Action<IAbpHangfireConfiguration> configureAction)
        {
            backgroundJobConfiguration.AbpConfiguration.ReplaceService<IBackgroundJobManager, HangfireBackgroundJobManager>();
            configureAction(backgroundJobConfiguration.AbpConfiguration.Modules.AbpHangfire());
        }
    }
}
