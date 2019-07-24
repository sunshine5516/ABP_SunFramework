using System;
using System.Collections.Generic;
namespace AbpFramework.Collections
{
    /// <summary>
    /// A shortcut for <see cref="ITypeList{TBaseType}"/> to use object as base type.
    /// </summary>
    public interface ITypeList : ITypeList<object>
    {

    }
    /// <summary>
    /// 扩展 <see cref="IList{Type}"/> 添加限制特定的基本类型。
    /// </summary>
    /// <typeparam name="TBaseType"></typeparam>
    public interface ITypeList<in TBaseType>:IList<Type>
    {
        void Add<T>() where T : TBaseType;
        bool Contains<T>() where T : TBaseType;
        void Remove<T>() where T : TBaseType;
    }
}
