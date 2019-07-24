using AbpFramework.Configuration;
using System.Collections.Generic;
namespace AbpFramework.Notifications
{
    public class NotificationSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                new SettingDefinition(
                    NotificationSettingNames.ReceiveNotifications,
                    "true",
                    "ReceiveNotifications",
                    scopes:SettingScopes.User,
                    isVisibleToClients:true
                    )
            };
        } 
    }
}
