namespace AbpFramework.BackgroundJobs
{
    /// <summary>
    /// 定义一个后台工作任务的接口
    /// </summary>
    public interface IBackgroundJob<in TArgs>
    {
        void Execute(TArgs args);
    }
}
