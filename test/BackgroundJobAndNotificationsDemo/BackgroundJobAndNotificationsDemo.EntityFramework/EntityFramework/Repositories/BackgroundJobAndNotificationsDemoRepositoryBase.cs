using Abp.EntityFramework.EntityFramework;
using Abp.EntityFramework.EntityFramework.Repositories;
using AbpFramework.Domain.Entities;
namespace BackgroundJobAndNotificationsDemo.EntityFramework.EntityFramework.Repositories
{
    public abstract class BackgroundJobAndNotificationsDemoRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<BackgroundJobAndNotificationsDemoDbContext, TEntity, TPrimaryKey>
       where TEntity : class, IEntity<TPrimaryKey>
    {
        protected BackgroundJobAndNotificationsDemoRepositoryBase(IDbContextProvider<BackgroundJobAndNotificationsDemoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }
    }
    public abstract class BackgroundJobAndNotificationsDemoRepositoryBase<TEntity> : BackgroundJobAndNotificationsDemoRepositoryBase<TEntity, int>
    where TEntity : class, IEntity<int>
    {
        protected BackgroundJobAndNotificationsDemoRepositoryBase(IDbContextProvider<BackgroundJobAndNotificationsDemoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
