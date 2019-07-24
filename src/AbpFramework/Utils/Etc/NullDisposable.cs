using System;
namespace AbpFramework.Utils.Etc
{
    /// <summary>
    /// 用于模拟不执行任何操作的Disposable。
    /// </summary>
    internal sealed class NullDisposable : IDisposable
    {
        public static NullDisposable Instance { get; } = new NullDisposable();

        private NullDisposable()
        {

        }
        public void Dispose()
        {
            
        }
    }
}
