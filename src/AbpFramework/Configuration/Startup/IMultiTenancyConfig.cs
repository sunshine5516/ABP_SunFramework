namespace AbpFramework.Configuration.Startup
{
    /// <summary>
    /// 多租户配置
    /// </summary>
    public interface IMultiTenancyConfig
    {
        /// <summary>
        /// 是否可用；默认false
        /// </summary>
        bool IsEnabled { get; set; }
        //ITypeList<ITenantResolveContributor> Resolvers { get; }
    }
}
