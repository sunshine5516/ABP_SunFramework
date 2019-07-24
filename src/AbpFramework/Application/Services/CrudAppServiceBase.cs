using AbpFramework.Application.Services.Dto;
using AbpFramework.Domain.Entities;
using AbpFramework.Domain.Repositories;
using AbpFramework.Extensions;
using System.Linq;
using System.Linq.Dynamic.Core;
using AbpFramework.Linq.Extensions;
using AbpFramework.Authorization;

namespace AbpFramework.Application.Services
{
    /// <summary>
    /// CrudAppService and AsyncCrudAppService基类.
    /// Inherit either from CrudAppService or AsyncCrudAppService, not from this class.
    /// </summary>
    public abstract class CrudAppServiceBase<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput> : ApplicationService
       where TEntity : class, IEntity<TPrimaryKey>
       where TEntityDto : IEntityDto<TPrimaryKey>
       where TUpdateInput : IEntityDto<TPrimaryKey>
    {
        #region 声明实例
        protected readonly IRepository<TEntity, TPrimaryKey> Repository;

        protected virtual string GetPermissionName { get; set; }

        protected virtual string GetAllPermissionName { get; set; }

        protected virtual string CreatePermissionName { get; set; }

        protected virtual string UpdatePermissionName { get; set; }

        protected virtual string DeletePermissionName { get; set; }
        #endregion
        #region 构造函数
        protected CrudAppServiceBase(IRepository<TEntity,TPrimaryKey> repository)
        {
            Repository = repository;
        }
        #endregion
        #region 方法
        protected virtual IQueryable<TEntity> ApplySorting(IQueryable<TEntity> query, TGetAllInput input)
        {
            var sortInput = input as ISortedResultRequest;
            if(sortInput!=null)
            {
                if (!sortInput.Sorting.IsNullOrWhiteSpace())
                {
                    return query.OrderBy(sortInput.Sorting);
                }
            }
            if (input is ILimitedResultRequest)
            {
                return query.OrderByDescending(e => e.Id);
            }

            //No sorting
            return query;
        }
        public virtual IQueryable<TEntity> ApplyPaging(IQueryable<TEntity> query, TGetAllInput input)
        {
            var pagedInput = input as IPagedResultRequest;
            if (pagedInput != null)
            {
                return query.PageBy(pagedInput);
            }
            var limitedInput = input as ILimitedResultRequest;
            if (limitedInput != null)
            {
                return query.Take(limitedInput.MaxResultCount);
            }
            return query;
        }
        protected virtual IQueryable<TEntity> CreateFilteredQuery(TGetAllInput input)
        {
            return Repository.GetAll();
        }
        /// <summary>
        /// 把<see cref="TEntity"/>转换为<see cref="TEntityDto"/>.
        /// 默认使用<see cref="IObjectMapper"/>.
        /// </summary>
        protected virtual TEntityDto MapToEntityDto(TEntity entity)
        {
            return ObjectMapper.Map<TEntityDto>(entity);
        }
        /// <summary>
        /// 把 <see cref="TEntityDto"/> 转换为 <see cref="TEntity"/>.
        /// 默认使用<see cref="IObjectMapper"/>.
        /// </summary>
        protected virtual TEntity MapToEntity(TCreateInput createInput)
        {
            return ObjectMapper.Map<TEntity>(createInput);
        }
        /// <summary>
        /// 把<see cref="TUpdateInput"/> 转换为 <see cref="TEntity"/>
        /// 默认使用<see cref="IObjectMapper"/>.
        /// </summary>
        protected virtual void MapToEntity(TUpdateInput updateInput, TEntity entity)
        {
            ObjectMapper.Map(updateInput, entity);
        }
        protected virtual void CheckPermission(string permissionName)
        {
            if(!string.IsNullOrEmpty(permissionName))
            {
                PermissionChecker.Authorize(permissionName);
            }
        }
        protected virtual void CheckGetPermission()
        {
            CheckPermission(GetPermissionName);
        }

        protected virtual void CheckGetAllPermission()
        {
            CheckPermission(GetAllPermissionName);
        }

        protected virtual void CheckCreatePermission()
        {
            CheckPermission(CreatePermissionName);
        }

        protected virtual void CheckUpdatePermission()
        {
            CheckPermission(UpdatePermissionName);
        }

        protected virtual void CheckDeletePermission()
        {
            CheckPermission(DeletePermissionName);
        }
        #endregion
    }
}
