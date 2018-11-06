using AutoMapper;
using System;
using System.Collections.Generic;
namespace Abp.AutoMapper.AutoMapper
{
    public interface IAbpAutoMapperConfiguration
    {
        bool UseStaticMapper { get; set; }
        List<Action<IMapperConfigurationExpression>> Configurators { get; }

    }
}
