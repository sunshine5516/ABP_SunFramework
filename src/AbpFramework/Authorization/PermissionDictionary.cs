using System;
using System.Collections.Generic;
using System.Linq;
namespace AbpFramework.Authorization
{
    /// <summary>
    /// 用于存储permission对象的字典
    /// </summary>
    class PermissionDictionary : Dictionary<string, Permission>
    {
        /// <summary>
        /// 递归地添加当前权限的所有子权限.
        /// </summary>
        public void AddAllPermissions()
        {
            foreach(var permission in Values.ToList())
            {
                AddPermissionRecursively(permission);
            }
        }
        /// <summary>
        /// 添加一个权限和子权限到字典。
        /// </summary>
        /// <param name="permission">Permission to be added</param>
        private void AddPermissionRecursively(Permission permission)
        {
            //Prevent multiple adding of same named permission.
            Permission existingPermission;
            if (TryGetValue(permission.Name, out existingPermission))
            {
                if (existingPermission != permission)
                {
                    throw new Exception("Duplicate permission name detected for " + permission.Name);
                }
            }
            else
            {
                this[permission.Name] = permission;
            }
            //Add child permissions (recursive call)
            foreach (var childPermission in permission.Children)
            {
                AddPermissionRecursively(childPermission);
            }
        }
    }
}
