using AbpFramework.Configuration.Startup;
namespace AbpFramework.BackgroundJobs
{
    public class BackgroundJobConfiguration : IBackgroundJobConfiguration
    {
        public bool IsJobExecutionEnabled { get; set; }

        public IAbpStartupConfiguration AbpConfiguration { get; private set; }
        public BackgroundJobConfiguration(IAbpStartupConfiguration abpConfiguration)
        {
            AbpConfiguration = abpConfiguration;

            IsJobExecutionEnabled = true;
        }
    }
}
