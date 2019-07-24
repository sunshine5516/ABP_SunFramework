namespace AbpFramework.Threading
{
    /// <summary>
    /// 启动或终止线程服务接口
    /// </summary>
    public interface IRunnable
    {
        /// <summary>
        /// 启动服务
        /// </summary>
        void Start();
        /// <summary>
        /// 终止服务
        /// 服务可能会立即返回并异步停止。
        /// 客户端应该调用<see cref ="WaitToStop"/>方法来确保它停止。
        /// </summary>
        void Stop();
        /// <summary>
        /// 等待线程终止
        /// </summary>
        void WaitToStop();
    }
}
