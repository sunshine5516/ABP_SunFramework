namespace AbpFramework.ObjectMapping
{
    public class NullObjectMapper : IObjectMapper
    {
        #region 声明实例
        public static NullObjectMapper Instance { get { return SingletonInstance; } }
        private static readonly NullObjectMapper SingletonInstance = new NullObjectMapper();
        #endregion
        #region 构造函数

        #endregion
        #region 方法
        public TDestination Map<TDestination>(object source)
        {
            throw new AbpException("Abp.ObjectMapping.IObjectMapper should be implemented in order to map objects.");
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            throw new AbpException("Abp.ObjectMapping.IObjectMapper should be implemented in order to map objects.");
        }
        #endregion

    }
}
