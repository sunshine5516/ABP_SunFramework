namespace AbpFramework
{
    /// <summary>
    /// 用于定义ABP的一些常量
    /// </summary>
    public static  class AbpConsts
    {
        public const string LocalizationSourceName = "Abp";
        public static class Orms
        {
            public const string Dapper = "Dapper";
            public const string EntityFramework = "EntityFramework";
            public const string EntityFrameworkCore = "EntityFrameworkCore";
            public const string NHibernate = "NHibernate";
        }
    }
}
