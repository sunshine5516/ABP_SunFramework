namespace AbpFramework.ObjectMapping
{
    /// <summary>
    /// 定义一个映射对象的简单接口
    /// </summary>
    public interface IObjectMapper
    {
        /// <summary>
        /// 将对象转换为另一个对象。 创建<see cref ="TDestination"/>的新对象。
        /// </summary>
        /// <typeparam name="TDestination">目标对象的类型</typeparam>
        /// <param name="source">源对象</param>
        /// <returns></returns>
        TDestination Map<TDestination>(object source);
        /// <summary>
        /// 执行从源对象到现有目标对象的映射
        /// </summary>
        /// <typeparam name="TSource">源对象</typeparam>
        /// <typeparam name="TDestination">目标对象的类型</typeparam>
        /// <param name="source">源对象</param>
        /// <param name="destination">目标对象的类型</param>
        /// <returns>Returns the same <see cref="destination"/> object after mapping operation</returns>
        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
    }
}
