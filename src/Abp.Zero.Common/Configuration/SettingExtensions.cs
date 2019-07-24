using AbpFramework.Configuration;
namespace Abp.Zero.Common.Configuration
{
    /// <summary>
    /// 实现在SettingInfo和Setting类之间转换对象的方法。
    /// </summary>
    internal static class SettingExtensions
    {
        public static Setting ToSetting(this SettingInfo settingInfo)
        {
            return settingInfo == null
                ? null : new Setting(settingInfo.TenantId, settingInfo.UserId, settingInfo.Name, settingInfo.Value);

        }
        public static SettingInfo ToSettingInfo(this Setting setting)
        {
            return setting == null
                ? null : new SettingInfo(setting.TenantId, setting.UserId, setting.Name, setting.Value);
        }
    }
}
