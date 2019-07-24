using System;
using System.Collections.Generic;
namespace Abp.Zero.Common.Zero.Configuration
{
    public class RoleManagementConfig : IRoleManagementConfig
    {
        public List<StaticRoleDefinition> StaticRoles { get; private set; }
        public RoleManagementConfig()
        {
            StaticRoles = new List<StaticRoleDefinition>();
        }
    }
}
