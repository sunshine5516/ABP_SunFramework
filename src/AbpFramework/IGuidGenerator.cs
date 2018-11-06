using System;
namespace AbpFramework
{
    /// <summary>
    /// 用于生成ID
    /// </summary>
    public interface IGuidGenerator
    {
        Guid Create();
    }
}
