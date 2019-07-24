using AbpFramework.Threading.Timers;
using System;
namespace AbpFramework.Threading.BackgroundWorkers
{
    /// <summary>
    /// 通过封装AbpTimer实现定时启动执行任务的功能。
    /// 这个类型定义个一个抽象方法DoWork. AbpTimer最终会定时执行这个方法。
    /// </summary>
    public abstract class PeriodicBackgroundWorkerBase: BackgroundWorkerBase
    {
        #region 声明实例
        protected readonly AbpTimer Timer;
        #endregion

        #region 构造函数
        protected PeriodicBackgroundWorkerBase(AbpTimer timer)
        {
            Timer = timer;
            Timer.Elapsed += Timer_Elapsed;
        }
        #endregion
        #region 方法
        public override void Start()
        {
            base.Start();
            Timer.Start();
        }
        public override void Stop()
        {
            Timer.Stop();
            base.Stop();
        }
        public override void WaitToStop()
        {
            Timer.WaitToStop();
            base.WaitToStop();
        }
        private void Timer_Elapsed(object sender, EventArgs e)
        {
            try
            {
                DoWork();
            }
            catch(Exception ex)
            {
                Logger.Warn(ex.ToString(), ex);
            }
        }
        /// <summary>
        /// 执行的任务
        /// </summary>
        protected abstract void DoWork();
        #endregion

    }
}
