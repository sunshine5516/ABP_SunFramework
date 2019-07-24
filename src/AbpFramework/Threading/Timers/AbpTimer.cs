using AbpFramework.Dependency;
using System;
using System.Threading;

namespace AbpFramework.Threading.Timers
{
    public class AbpTimer : RunnableBase, ITransientDependency
    {
        #region 声明实例
        /// <summary>
        /// 该事件根据定时器的周期定期提高
        /// </summary>
        public event EventHandler Elapsed;
        /// <summary>
        /// 定时器的任务周期（以毫秒为单位）。
        /// </summary>
        public int Period { get; set; }
        /// <summary>
        /// 指示timer是否在Timer的Start方法中引发Elapsed事件一次。
        /// 默认false
        /// </summary>
        public bool RunOnStart { get; set; }
        /// <summary>
        /// 该计时器用于以指定的时间间隔执行任务。
        /// </summary>
        private readonly Timer _taskTimer;
        /// <summary>
        /// 定时器是否在运行
        /// </summary>
        private volatile bool _runging;
        /// <summary>
        /// 表示执行任务或_taskTimer是否处于睡眠模式。.
        /// 此字段用于在停止Timer时等待正在执行的任务.
        /// </summary>
        private volatile bool _performingTasks;
        #endregion
        #region 构造函数
        public AbpTimer()
        {
            _taskTimer = new Timer(TimerCallBack, null, Timeout.Infinite, Timeout.Infinite);
        }
        public AbpTimer(int period, bool runOnStart = false) : this()
        {
            Period = period;
            RunOnStart = runOnStart;
        }
        #endregion
        #region 方法
        /// <summary>
        /// 启动定时器
        /// </summary>
        public override void Start()
        {
            if (Period <= 0)
            {
                throw new AbpException("Period should be set before starting the timer!");
            }
            base.Start();
            _runging = true;
            _taskTimer.Change(RunOnStart ? 0 : Period, Timeout.Infinite);
        }
        /// <summary>
        /// Stops the timer.
        /// 如何知道一个Timer真正结束了呢？也就是说如何知道一个Timer要执行的任务已经完成（这里定义为A效果）
        /// ，同时timer已失效(这里定义为B效果)？ABP通过stop方法实现B，通过WaitToStop实现A效果。
        /// WaitToStop会一直阻塞调用他的线程直到_performingTasks变成false,
        /// 也就是说Timer要执行的任务已经完成（任务完成时会将_performingTasks设为False，并且释放锁）。
        /// </summary>
        public override void Stop()
        {
            lock (_taskTimer)
            {
                _runging = false;
                _taskTimer.Change(Timeout.Infinite, Timeout.Infinite);
            }
            base.Stop();
        }
        public override void WaitToStop()
        {
            lock (_taskTimer)
            {
                while (_performingTasks)
                {
                    Monitor.Wait(_taskTimer);
                }
            }
            base.WaitToStop();
        }
        private void TimerCallBack(object state)
        {
            lock (_taskTimer)
            {
                if(!_runging||_performingTasks)
                {
                    return;
                }
                _taskTimer.Change(Timeout.Infinite, Timeout.Infinite);
                _performingTasks = true;
            }
            try
            {
                if(Elapsed!=null)
                {
                    Elapsed(this, new EventArgs());
                }
            }
            catch
            {

            }
            finally
            {
                lock(_taskTimer)
                {
                    _performingTasks = false;
                    if(_runging)
                    {
                        _taskTimer.Change(Period, Timeout.Infinite);
                    }
                    Monitor.Pulse(_taskTimer);
                }
            }
        }
        #endregion
    }
}
