﻿using AbpFramework.Application.Navigation;
using AbpFramework.Collections;

namespace AbpFramework.Configuration.Startup
{
    internal class NavigationConfiguration : INavigationConfiguration
    {
        public ITypeList<NavigationProvider> Providers { get; private set; }
        public NavigationConfiguration()
        {
            Providers = new TypeList<NavigationProvider>();
        }
    }
}