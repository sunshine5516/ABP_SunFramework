using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Configuration
{
    /// <summary>
    /// 定义接口以使用字典进行配置
    /// </summary>
    public interface IDictionaryBasedConfig
    {
        /// <summary>
        /// 设置。如果有则覆盖
        /// </summary>
        /// <param name="name">配置唯一名称</param>
        /// <param name="value">配置信息</param>
        void Set<T>(string name, T value);
        object Get(string name);

        /// <summary>
        /// 根据名称获取配置信息
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="name">配置唯一名称</param>
        T Get<T>(string name);

        /// <summary>
        /// 根据名称获取配置信息
        /// </summary>
        /// <param name="name">配置唯一名称</param>
        /// <param name="defaultValue">默认值</param>
        object Get(string name, object defaultValue);

        /// <summary>
        /// 根据名称获取配置信息.
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="name">配置唯一名称</param>
        /// <param name="defaultValue">默认值</param>
        T Get<T>(string name, T defaultValue);

        /// <summary>
        /// 根据名称获取配置信息
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="name">配置唯一名称</param>
        /// <param name="creator">The function that will be called to create if given configuration is not found</param>
        /// <returns>Value of the configuration or null if not found</returns>
        T GetOrCreate<T>(string name, Func<T> creator);

    }
}
