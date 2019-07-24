using AbpFramework.Configuration.Startup;
namespace AbpFramework.BackgroundJobs
{
    /// <summary>
    /// 配置后台作业系统接口
    /// </summary>
    public interface IBackgroundJobConfiguration
    {
        /// <summary>
        /// 是否可用
        /// </summary>
        bool IsJobExecutionEnabled { get; set; }
        /// <summary>
        /// 获取ABP配置对象
        /// </summary>
        IAbpStartupConfiguration AbpConfiguration { get; }
    }
}
