using Abp.MultiTenancy;
using Abp.Zero.Authorization.Roles;
using Abp.Zero.Authorization.Users;
using Abp.Zero.Common.Application.Editions;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
namespace Abp.Zero.EntityFramework
{
    /// <summary>
    /// ABP zero模块上下文基类.
    /// 从此类派生您的DbContext以具有基本实体。
    /// </summary>
    public abstract class AbpZeroDbContext<TTenant, TRole, TUser> :
        AbpZeroCommonDbContext<TRole, TUser>
        where TTenant : AbpTenant<TUser>
        where TRole : AbpRole<TUser>
        where TUser : AbpUser<TUser>
    {
        public virtual IDbSet<TTenant> Tenants { get; set; }
        public virtual IDbSet<Edition> Editions { get; set; }
        protected AbpZeroDbContext()
        { }
        protected AbpZeroDbContext(string nameOrConnectionString)
           : base(nameOrConnectionString)
        {

        }
        protected AbpZeroDbContext(DbCompiledModel model)
            : base(model)
        {

        }

        protected AbpZeroDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {

        }

        protected AbpZeroDbContext(string nameOrConnectionString, DbCompiledModel model)
            : base(nameOrConnectionString, model)
        {
        }

        protected AbpZeroDbContext(ObjectContext objectContext, bool dbContextOwnsObjectContext)
            : base(objectContext, dbContextOwnsObjectContext)
        {
        }

        protected AbpZeroDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
        }
    }
}
