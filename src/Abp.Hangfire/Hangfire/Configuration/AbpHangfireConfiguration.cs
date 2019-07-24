using HangfireGlobalConfiguration = Hangfire.GlobalConfiguration;
using Hangfire;

namespace Abp.Hangfire.Hangfire.Configuration
{
    public class AbpHangfireConfiguration : IAbpHangfireConfiguration
    {
        public BackgroundJobServer Server { get; set; }

        public IGlobalConfiguration GlobalConfiguration
        {
            get { return HangfireGlobalConfiguration.Configuration; }
        }
    }
}
