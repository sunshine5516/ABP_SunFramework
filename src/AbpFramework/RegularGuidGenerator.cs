using AbpFramework.Dependency;
using System;
namespace AbpFramework
{
    public class RegularGuidGenerator : IGuidGenerator, ITransientDependency
    {
        public virtual Guid Create()
        {
            return Guid.NewGuid();
        }
    }
}
