using AutoMapper;
namespace AbpDemo.Application
{
    public interface IDtoMapping
    {
        void CreateMapping(IMapperConfigurationExpression mapperConfig);
    }
}
