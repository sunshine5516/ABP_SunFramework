using System;
namespace Abp.EntityFramework.Common
{
    public interface IDbContextTypeMatcher
    {
        void Populate(Type[] dbContextTypes);
        Type GetConcreteType(Type sourceDbContextType);
    }
}
