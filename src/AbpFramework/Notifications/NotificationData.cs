using AbpFramework.Collections.Extensions;
using AbpFramework.Json;
using System;
using System.Collections.Generic;
namespace AbpFramework.Notifications
{
    /// <summary>
    /// 用于储存真正的Notification的数据(即内容)
    /// </summary>
    [Serializable]
    public class NotificationData
    {
        /// <summary>
        /// 通知类型名称.
        /// 默认返回类全名
        /// </summary>
        public virtual string Type => GetType().FullName;
        private readonly Dictionary<string, object> _properties;
        public object this[string key]
        {
            get { return Properties.GetOrDefault(key); }
            set { Properties[key] = value; }
        }
        /// <summary>
        /// 用于向此通知添加自定义属性.
        /// </summary>
        public Dictionary<string, object> Properties
        {
            get { return _properties; }
            set
            {
                if(value==null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                foreach(var keyValue in value)
                {
                    if(!_properties.ContainsKey(keyValue.Key))
                    {
                        _properties[keyValue.Key] = keyValue.Value;
                    }
                }
            }
        }
        public NotificationData()
        {
            _properties = new Dictionary<string, object>();
        }
        public override string ToString()
        {
            return this.ToJsonString();
        }
    }
}
