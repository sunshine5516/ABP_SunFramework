using System.Collections.Generic;
namespace AbpFramework.Application.Navigation
{
    /// <summary>
    /// 封装了用于显示给用户的菜单
    /// </summary>
    public class UserMenu
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public object CustomData { get; set; }
        public IList<UserMenuItem> Items { get; set; }
        #region 构造函数
        public UserMenu()
        {
        }
        internal UserMenu(MenuDefinition menuDefinition)
        {
            Name = menuDefinition.Name;
            DisplayName = menuDefinition.DisplayName;
            CustomData=menuDefinition.CustomData;
            Items = new List<UserMenuItem>();
        }
        #endregion
    }
}
