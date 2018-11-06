using System;
using System.Collections.Generic;
namespace AbpFramework.Auditing
{
    public class AuditingConfiguration : IAuditingConfiguration
    {
        public bool IsEnabled { get; set; }
        public bool IsEnabledForAnonymousUsers { get; set; }

        public List<Type> IgnoredTypes { get; }

        public IAuditingSelectorList Selectors { get; }
        public AuditingConfiguration()
        {
            IsEnabled = true;
            Selectors = new AuditingSelectorList();
            IgnoredTypes = new List<Type>();
        }
    }
}
