using Castle.Core;
namespace AbpFramework
{
    /// <summary>
    /// 需要在使用前初始化的对象的接口
    /// 如果使用依赖注入解析对象，<see cref ="IInitializable.Initialize"/>
    /// 在创建对象后立即自动调用方法。
    /// </summary>
    public interface IShouldInitialize: IInitializable
    {
    }
}
