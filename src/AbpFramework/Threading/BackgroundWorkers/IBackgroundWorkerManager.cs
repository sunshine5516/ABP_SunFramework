namespace AbpFramework.Threading.BackgroundWorkers
{
    /// <summary>
    /// 管理后台工作任务接口
    /// </summary>
    public interface IBackgroundWorkerManager: IRunnable
    {
        /// <summary>
        /// 添加一个新的工作者。 如果<see cref ="IBackgroundWorkerManager"/>已经启动，立即启动工作。
        /// </summary>
        /// <param name="worker">
        /// The worker. It should be resolved from IOC.
        /// </param>
        void Add(IBackgroundWorker worker);
    }
}
