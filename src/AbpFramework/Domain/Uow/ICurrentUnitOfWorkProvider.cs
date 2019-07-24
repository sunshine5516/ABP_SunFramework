namespace AbpFramework.Domain.Uow
{
    /// <summary>
    /// 获取/设置当前<see cref="IUnitOfWork"/>. 
    /// </summary>
    public interface ICurrentUnitOfWorkProvider
    {
        /// <summary>
        /// 获取/设置当前<see cref="IUnitOfWork"/>. 
        /// </summary>
        IUnitOfWork Current { get; set; }
    }
}
