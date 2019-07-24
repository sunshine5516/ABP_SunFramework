namespace AbpFramework.Runtime.Session
{
    public static class AbpSessionExtensions
    {
        /// <summary>
        /// 获取当前用户Id
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static long GetUserId(this IAbpSession session)
        {
            if (!session.UserId.HasValue)
            {
                throw new AbpException("Session.UserId is null! Probably, user is not logged in.");
            }
            return session.UserId.Value;
            //return 11;
        }
        /// <summary>
        /// 获取当前 Tenant's Id
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static int GetTenantId(this IAbpSession session)
        {
            if (!session.TenantId.HasValue)
            {
                throw new AbpException("Session.TenantId is null! Possible problems: No user logged in or current logged in user in a host user (TenantId is always null for host users).");
            }

            return session.TenantId.Value;
        }
        public static UserIdentifier ToUserIdentifier(this IAbpSession session)
        {
            return session.UserId == null
                ? null
                : new UserIdentifier(session.TenantId, session.GetUserId());
        }
    }
}
