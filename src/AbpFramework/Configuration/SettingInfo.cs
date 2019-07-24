using System;
namespace AbpFramework.Configuration
{
    /// <summary>
    /// Represents a setting information.
    /// </summary>
    [Serializable]
    public class SettingInfo
    {
        /// <summary>
        /// 租户ID.
        /// 如果不是租户级别的则为null
        /// </summary>
        public int? TenantId { get; set; }
        /// <summary>
        /// 用户ID.
        /// 如果不是用户级别的则为null
        /// </summary>
        public long? UserId { get; set; }
        /// <summary>
        /// 唯一名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        public SettingInfo()
        {
        }
        public SettingInfo(int?tenantId,long? userId,string name,string value)
        {
            TenantId = tenantId;
            UserId = userId;
            Name = name;
            Value = value;
        }
    }
}
