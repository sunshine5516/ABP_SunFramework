namespace AbpFramework.Application.Navigation
{
    /// <summary>
    /// 提供设置导航的基础架构。
    /// </summary>
    public interface INavigationProviderContext
    {
        /// <summary>
        /// <see cref="INavigationManager"/>实例
        /// </summary>
        INavigationManager Manager { get; }
    }
}
