namespace Abp.Zero.Common.Authorization
{
    public class PermissionGrantInfo
    {
        public string Name { get; set; }
        public bool IsGranted { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="isGranted"></param>
        public PermissionGrantInfo(string name, bool isGranted)
        {
            Name = name;
            IsGranted = isGranted;
        }
    }
}
