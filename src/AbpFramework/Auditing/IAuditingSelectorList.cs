using System.Collections.Generic;
namespace AbpFramework.Auditing
{
    /// <summary>
    /// NamedTypeSelector对象的集合
    /// </summary>
    public interface IAuditingSelectorList : IList<NamedTypeSelector>
    {
        bool RemoveByName(string name);
    }
}
