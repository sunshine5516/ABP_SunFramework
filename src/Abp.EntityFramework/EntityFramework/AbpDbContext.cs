using AbpFramework;
using AbpFramework.Configuration.Startup;
using AbpFramework.Dependency;
using AbpFramework.Domain.Entities;
using AbpFramework.Domain.Uow;
using AbpFramework.Extensions;
using AbpFramework.Reflection;
using AbpFramework.Runtime.Session;
using Castle.Core.Logging;
using EntityFramework.DynamicFilters;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Abp.EntityFramework.EntityFramework
{
    /// <summary>
    /// 应用程序中所有DbContext类的基类。
    /// </summary>
    public abstract class AbpDbContext : DbContext, ITransientDependency, IShouldInitialize
    {
        #region 声明实例
        /// <summary>
        /// 获取当前session值.
        /// </summary>
        public IAbpSession AbpSession { get; set; }
        /// <summary>
        /// 用于触发实体更改事件
        /// </summary>
        //public IEntityChangeEventHelper EntityChangeEventHelper { get; set; }
        public ILogger Logger { get; set; }
        /// <summary>
        /// Reference to the event bus.
        /// </summary>
        //public IEventBus EventBus { get; set; }
        //GUID 生成器.
        public IGuidGenerator GuidGenerator { get; set; }
        /// <summary>
        /// UOW provider引用
        /// </summary>
        public ICurrentUnitOfWorkProvider CurrentUnitOfWorkProvider { get; set; }
        public IMultiTenancyConfig MultiTenancyConfig { get; set; }
        /// <summary>
        /// 可用于禁止在SaveChanges上自动设置TenantId。
        /// </summary>
        public bool SuppressAutoSetTenantId { get; set; }
        #endregion
        #region 构造函数
        protected AbpDbContext()
        {
            InitializeDbContext();
        }
        protected AbpDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            InitializeDbContext();
        }

        protected AbpDbContext(DbCompiledModel model)
            : base(model)
        {
            InitializeDbContext();
        }
        protected AbpDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
            InitializeDbContext();
        }
        protected AbpDbContext(string nameOrConnectionString, DbCompiledModel model)
           : base(nameOrConnectionString, model)
        {
            InitializeDbContext();
        }
        protected AbpDbContext(ObjectContext objectContext, bool dbContextOwnsObjectContext)
            : base(objectContext, dbContextOwnsObjectContext)
        {
            InitializeDbContext();
        }
        protected AbpDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
            InitializeDbContext();
        }
        #endregion
        #region 方法
        private void InitializeDbContext()
        {
            SetNullsForInjectedProperties();
            RegisterToChanges();
        }

        private void RegisterToChanges()
        {
            ((IObjectContextAdapter) this)
                .ObjectContext
                .ObjectStateManager
                .ObjectStateManagerChanged+= ObjectStateManager_ObjectStateManagerChanged;
        }

        protected virtual void ObjectStateManager_ObjectStateManagerChanged(object sender,
            CollectionChangeEventArgs e)
        {
            var contextAdapter = (IObjectContextAdapter)this;
            if (e.Action != CollectionChangeAction.Add)
            {
                return;
            }
            var entry = contextAdapter.ObjectContext.ObjectStateManager.GetObjectStateEntry(e.Element);
            switch (entry.State)
            {
                case EntityState.Added:
                    CheckAndSetId(entry.Entity);
                    CheckAndSetMustHaveTenantIdProperty(entry.Entity);
                    //SetCreationAuditProperties(entry.Entity, GetAuditUserId());
                    break;
                    //case EntityState.Deleted: //It's not going here at all
                    //    SetDeletionAuditProperties(entry.Entity, GetAuditUserId());
                    //    break;
            }            
        }

        private void SetNullsForInjectedProperties()
        {
            Logger = NullLogger.Instance;
            AbpSession = NullAbpSession.Instance;
            //EntityChangeEventHelper = NullEntityChangeEventHelper.Instance;
            //GuidGenerator = RegularGuidGenerator;
            //EventBus = NullEventBus.Instance;
        }

        public virtual void Initialize()
        {
            Database.Initialize(false);
            this.SetFilterScopedParameterValue(AbpDataFilters.MustHaveTenant, AbpDataFilters.Parameters.TenantId,
               AbpSession.TenantId ?? 0);
            this.SetFilterScopedParameterValue(AbpDataFilters.MayHaveTenant, AbpDataFilters.Parameters.TenantId,
                AbpSession.TenantId);            
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Filter(AbpDataFilters.SoftDelete, (ISoftDelete d) => d.IsDeleted, false);
            modelBuilder.Filter(AbpDataFilters.MustHaveTenant,
                (IMustHaveTenant t, int tenantId) => t.TenantId == tenantId || (int?)t.TenantId == null,
                0); //While "(int?)t.TenantId == null" seems wrong, it's needed. See https://github.com/jcachat/EntityFramework.DynamicFilters/issues/62#issuecomment-208198058
            modelBuilder.Filter(AbpDataFilters.MayHaveTenant,
                (IMayHaveTenant t, int? tenantId) => t.TenantId == tenantId, 0);
        }
        public override int SaveChanges()
        {
            return base.SaveChanges();
            //try
            //{
            //    var changedEntities = ApplyAbpConcepts();
            //    var result = base.SaveChanges();
            //    EntityChangeEventHelper.TriggerEvents(changedEntities);
            //    return result;
            //}
            //catch (DbEntityValidationException ex)
            //{
            //    LogDbEntityValidationException(ex);
            //    throw;
            //}
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken);
            //try
            //{
            //    var changeReport = ApplyAbpConcepts();
            //    var result = await base.SaveChangesAsync(cancellationToken);
            //    await EntityChangeEventHelper.TriggerEventsAsync(changeReport);
            //    return result;
            //}
            //catch (DbEntityValidationException ex)
            //{
            //    LogDbEntityValidationException(ex);
            //    throw;
            //}
        }

        //protected virtual EntityChangeReport ApplyAbpConcepts()
        //{
        //    throw new NotImplementedException();
        //}
        #endregion
        #region 辅助方法
        //protected virtual EntityChangeReport ApplyAbpConcepts()
        //{

        //}
        protected virtual void CheckAndSetId(object entityAsObj)
        {
            var entity=entityAsObj as IEntity<Guid>;
            if (entity != null && entity.Id == Guid.Empty)
            {
                var entityType = ObjectContext.GetObjectType(entityAsObj.GetType());
                var idProperty = entityType.GetProperty("Id");
                var dbGeneratedAttr =
                    ReflectionHelper.GetSingleAttributeOrDefault<DatabaseGeneratedAttribute>(idProperty);
                if (dbGeneratedAttr == null || dbGeneratedAttr.DatabaseGeneratedOption == DatabaseGeneratedOption.None)
                {
                    entity.Id = GuidGenerator.Create();
                }
            }
        }
        protected virtual void CheckAndSetMustHaveTenantIdProperty(object entityAsObj)
        {
            if (SuppressAutoSetTenantId)
            {
                return;
            }
            //Only set IMustHaveTenant entities
            if (!(entityAsObj is IMustHaveTenant))
            {
                return;
            }

            var entity = entityAsObj.As<IMustHaveTenant>();

            //Don't set if it's already set
            if (entity.TenantId != 0)
            {
                return;
            }

            var currentTenantId = GetCurrentTenantIdOrNull();

            if (currentTenantId != null)
            {
                entity.TenantId = currentTenantId.Value;
            }
            else
            {
                throw new AbpException("Can not set TenantId to 0 for IMustHaveTenant entities!");
            }
        }
        protected virtual int? GetCurrentTenantIdOrNull()
        {
            if (CurrentUnitOfWorkProvider?.Current != null)
            {
                return CurrentUnitOfWorkProvider.Current.GetTenantId();
            }

            return AbpSession.TenantId;
        }
        protected virtual void SetCreationAuditProperties(object entityAsObj, long? userId)
        {
            //EntityAuditingHelper.SetCreationAuditProperties(MultiTenancyConfig, entityAsObj, AbpSession.TenantId,
            //    userId);
        }
        #endregion

    }
}
