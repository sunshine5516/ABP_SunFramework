using AbpDemo.Application.Roles.Dto;
using System.Collections.Generic;
using System.Linq;
namespace AbpDemo.Web.Models.Roles
{
    public class EditRoleModalViewModel
    {
        public RoleDto Role { get; set; }
        public IReadOnlyList<PermissionDto> Permissions { get; set; }
        public bool HasPermission(PermissionDto permission)
        {
            return Permissions != null && Role.Permissions.Any(p => p == permission.Name);
        }
    }
}