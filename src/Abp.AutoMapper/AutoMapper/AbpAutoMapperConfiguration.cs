using System;
using System.Collections.Generic;
using AutoMapper;
namespace Abp.AutoMapper.AutoMapper
{
    public class AbpAutoMapperConfiguration : IAbpAutoMapperConfiguration
    {
        public bool UseStaticMapper { get; set; }

        public List<Action<IMapperConfigurationExpression>> Configurators { get; }
        public AbpAutoMapperConfiguration()
        {
            UseStaticMapper = true;
            Configurators = new List<Action<IMapperConfigurationExpression>>();
        }
    }
}
