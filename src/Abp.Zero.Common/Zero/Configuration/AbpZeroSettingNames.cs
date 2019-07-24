namespace Abp.Zero.Common.Zero.Configuration
{
    public static class AbpZeroSettingNames
    {
        public static class UserManagement
        {
            public static class UserLockOut
            {
                public const string IsEnabled = "Abp.Zero.UserManagement.UserLockOut.IsEnabled";

                /// <summary>
                /// "Abp.Zero.UserManagement.UserLockOut.MaxFailedAccessAttemptsBeforeLockout".
                /// </summary>
                public const string MaxFailedAccessAttemptsBeforeLockout = "Abp.Zero.UserManagement.UserLockOut.MaxFailedAccessAttemptsBeforeLockout";

                /// <summary>
                /// "Abp.Zero.UserManagement.UserLockOut.DefaultAccountLockoutSeconds".
                /// </summary>
                public const string DefaultAccountLockoutSeconds = "Abp.Zero.UserManagement.UserLockOut.DefaultAccountLockoutSeconds";
            }
        }
    }
}
