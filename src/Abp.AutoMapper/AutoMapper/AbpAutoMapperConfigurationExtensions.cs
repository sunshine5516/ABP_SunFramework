using AbpFramework.Configuration.Startup;
namespace Abp.AutoMapper.AutoMapper
{
    public static class AbpAutoMapperConfigurationExtensions
    {
        public static IAbpAutoMapperConfiguration AbpAutoMapper(this IModuleConfigurations configurations)
        {
            return configurations.AbpConfiguration.Get<IAbpAutoMapperConfiguration>();
        }
    }
}
