using AbpFramework.Dependency;
namespace AbpFramework.Domain.Repositories
{
    /// <summary>
    /// 数据存储接口
    /// 此接口必须由所有存储库实现，以按约定标识它们。 实现通用版本而不是此版本。
    /// </summary>
    public interface IRepository: ITransientDependency
    {
    }
}
