using System;
namespace AbpFramework
{
    /// <summary>
    /// 类型选择器，这个对象的核心属性是一个以type为输入参数，返回bool类型的委托predicate
    /// </summary>
    public class NamedTypeSelector
    {
        /// <summary>
        /// Name of the selector.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Predicate.
        /// </summary>
        public Func<Type, bool> Predicate { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="predicate">Predicate</param>
        public NamedTypeSelector(string name, Func<Type, bool> predicate)
        {
            Name = name;
            Predicate = predicate;
        }
    }
}
