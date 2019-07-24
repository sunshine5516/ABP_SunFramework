namespace AbpFramework.Threading
{
    /// <summary>
    /// 启动或终止线程服务接口的实现
    /// </summary>
    public abstract class RunnableBase : IRunnable
    {
        public bool IsRunning { get { return _isRunning; } }
        private volatile bool _isRunning;
        public virtual void Start()
        {
            _isRunning = true;
        }

        public virtual void Stop()
        {
            _isRunning = false;
        }

        public virtual void WaitToStop()
        {
            
        }
    }
}
