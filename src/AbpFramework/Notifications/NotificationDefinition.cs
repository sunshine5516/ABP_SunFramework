using AbpFramework.Authorization;
using AbpFramework.Collections.Extensions;
using System;
using System.Collections.Generic;
namespace AbpFramework.Notifications
{
    /// <summary>
    /// 封装Notification Definnition 的信息.
    /// </summary>
    public class NotificationDefinition
    {
        /// <summary>
        /// 唯一名称
        /// </summary>
        public string Name { get;private  set; }
        /// <summary>
        /// 类型
        /// </summary>
        public Type EntityType { get; private set; }
        public string DisplayName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 权限依赖关系。 如果满足该依赖关系，则该通知将可供用户使用。
        /// Optional.
        /// </summary>
        public IPermissionDependency PermissionDependency { get; set; }
        public object this[string key]
        {
            get { return Attributes.GetOrDefault(key); }
            set { Attributes[key] = value; }
        }
        /// <summary>
        /// 与此对象相关的任意对象
        /// 对象必须是可序列化的
        /// </summary>
        public IDictionary<string, object> Attributes { get; private set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">唯一名称.</param>
        /// <param name="entityType">类型.</param>
        /// <param name="displayName">显示名称.</param>
        /// <param name="description">描述</param>
        /// <param name="permissionDependency">权限依赖关系。 如果满足该依赖关系，则该通知将可供用户使用。.</param>
        /// <param name="featureDependency">A feature dependency. This notification will be available to a tenant if this feature is enabled.</param>
        public NotificationDefinition(string name, Type entityType = null, string displayName = null,
            string description = null, IPermissionDependency permissionDependency = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name", "name can not be null, empty or whitespace!");
            }

            Name = name;
            EntityType = entityType;
            DisplayName = displayName;
            Description = description;
            PermissionDependency = permissionDependency;
            Attributes = new Dictionary<string, object>();
        }
    }
}
