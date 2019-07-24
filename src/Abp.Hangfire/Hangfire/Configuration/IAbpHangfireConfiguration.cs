using Hangfire;
namespace Abp.Hangfire.Hangfire.Configuration
{
    /// <summary>
    /// hangfire配置接口
    /// </summary>
    public interface IAbpHangfireConfiguration
    {
        BackgroundJobServer Server { get; set; }
        IGlobalConfiguration GlobalConfiguration { get; }
    }
}
