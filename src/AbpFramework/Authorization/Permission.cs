using AbpFramework.MultiTenancy;
using System.Collections.Generic;
using System.Collections.Immutable;
namespace AbpFramework.Authorization
{
    /// <summary>
    /// 用于定义一个Permission，一个permission可以包含多个子Permission.
    /// </summary>
    public sealed class Permission
    {
        /// <summary>
        /// 父类权限
        /// 如果设置，则只有在授予父母后才能授予此权限。
        /// </summary>
        public Permission Parent { get; private set; }
        /// <summary>
        /// 唯一名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 简单描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 哪一方可以使用此权限。
        /// </summary>
        public MultiTenancySides MultiTenancySides { get; set; }
        /// <summary>
        /// 子权限
        /// </summary>
        public IReadOnlyList<Permission> Children => _children.ToImmutableList();
        private readonly List<Permission> _children;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="displayName">显示名称</param>
        /// <param name="description">描述</param>
        /// <param name="multiTenancySides">Which side can use this permission</param>
        /// <param name="featureDependency">Depended feature(s) of this permission</param>
        public Permission(
            string name,
            string displayName = null,
            string description = null,
            MultiTenancySides multiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant)
        {
            if (name == null)
            {
                
            }

            Name = name;
            DisplayName = displayName;
            Description = description;
            MultiTenancySides = multiTenancySides;
            //FeatureDependency = featureDependency;

            _children = new List<Permission>();
        }
        /// <summary>
        /// 添加子权限
        /// 只有在授予父母后才能授予此权限
        /// </summary>
        /// <returns>子权限</returns>
        public Permission CreateChildPermission(
            string name,
            string displayName = null,string description = null,
            MultiTenancySides multiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant)
        {
            var permission=new Permission(name, displayName, description, multiTenancySides) { Parent = this };            
            _children.Add(permission);
            return permission;
        }
        public override string ToString()
        {
            return string.Format("[Permission: {0}]", Name);
        }
    }
}
