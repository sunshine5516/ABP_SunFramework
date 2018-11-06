using System;
namespace AbpFramework.Dependency
{
    /// <summary>
    /// 此接口用于在单个语句中包装批处理解析的范围
    /// </summary>
    public interface IScopedIocResolver : IIocResolver, IDisposable { }
}
