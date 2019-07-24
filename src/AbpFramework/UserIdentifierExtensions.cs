namespace AbpFramework
{
    public static class UserIdentifierExtensions
    {
        public static UserIdentifier ToUserIdentifier(this IUserIdentifier userIdentifier)
        {
            return new UserIdentifier(userIdentifier.TenantId, userIdentifier.UserId);
        }
    }
}
