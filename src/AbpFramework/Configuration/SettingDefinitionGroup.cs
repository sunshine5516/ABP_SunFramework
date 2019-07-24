using System.Collections.Generic;
using System.Collections.Immutable;
namespace AbpFramework.Configuration
{
    /// <summary>
    /// 用于给SettingDefinition分组
    /// 一个分组可以有子分组也可以是父分组
    /// </summary>
    public class SettingDefinitionGroup
    {
        /// <summary>
        /// 唯一名称
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 显示名称
        /// 用于展示给用户
        /// </summary>
        //public ILocalizableString DisplayName { get; private set; }
        public string DisplayName { get; private set; }
        /// <summary>
        /// 父级组
        /// </summary>
        public SettingDefinitionGroup Parent { get; private set; }
        /// <summary>
        /// 获取这个组的所有子列表。
        /// </summary>
        public IReadOnlyList<SettingDefinitionGroup> Children
        {
            get { return _children.ToImmutableList(); }
        }
        private readonly List<SettingDefinitionGroup> _children;
        /// <summary>
        /// 构造函数
        /// </summary>
        public SettingDefinitionGroup(string name, string displayName)
        {
            Name = name;
            DisplayName = displayName;
            _children = new List<SettingDefinitionGroup>();
        }
        public SettingDefinitionGroup AddChile(SettingDefinitionGroup child)
        {
            if (child.Parent != null)
            {
                throw new AbpException("Setting group " + child.Name + " has already a Parent (" + child.Parent.Name + ").");
            }
            _children.Add(child);
            child.Parent = this;
            return this;
        }
    }
}
