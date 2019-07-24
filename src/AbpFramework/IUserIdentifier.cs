namespace AbpFramework
{
    /// <summary>
    /// 用户标识接口
    /// </summary>
    public interface IUserIdentifier
    {
        int? TenantId { get; }
        long UserId { get; }
    }
}
