using AbpFramework.Collections.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Configuration
{
    public class DictionaryBasedConfig : IDictionaryBasedConfig
    {
        #region 声明实例
        /// <summary>
        /// 配置字典
        /// </summary>
        protected Dictionary<string, object> CustomSettings { get; private set; }
        /// <summary>
        /// 获取/设置配置值
        /// Returns null if no config with given name.
        /// </summary>
        /// <param name="name">Name of the config</param>
        /// <returns>Value of the config</returns>
        public object this[string name]
        {
            get { return CustomSettings.GetOrDefault(name); }
            set { CustomSettings[name] = value; }
        }
        #endregion
        #region 构造函数
        protected DictionaryBasedConfig()
        {
            CustomSettings = new Dictionary<string, object>();
        }
        #endregion
        #region 方法

        #endregion

        public object Get(string name)
        {
            return Get(name, null);
        }

        public T Get<T>(string name)
        {
            var value = this[name];
            return value == null
               ? default(T)
               : (T)Convert.ChangeType(value, typeof(T));
        }

        public object Get(string name, object defaultValue)
        {
            var value = this[name];
            if (name == null)
            {
                return defaultValue;
            }
            return value;
        }

        public T Get<T>(string name, T defaultValue)
        {
            return (T)Get(name, (object)defaultValue);
        }

        public T GetOrCreate<T>(string name, Func<T> creator)
        {
            var value = Get(name);
            if (value == null)
            {
                value = creator();
                Set(name, value);
            }
            return (T)value;
        }

        public void Set<T>(string name, T value)
        {
            this[name] = value;
        }
    }
}
