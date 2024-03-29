﻿using AbpFramework.Application.Services.Dto;
using AbpFramework.Domain.Entities;
using AbpFramework.Domain.Repositories;
using AbpFramework.Linq;
using System.Linq;
using System.Threading.Tasks;
namespace AbpFramework.Application.Services
{
    public abstract class AsyncCrudAppService<TEntity, TEntityDto>
      : AsyncCrudAppService<TEntity, TEntityDto, int>
      where TEntity : class, IEntity<int>
      where TEntityDto : IEntityDto<int>
    {
        protected AsyncCrudAppService(IRepository<TEntity, int> repository)
            : base(repository)
        {

        }
    }

    public abstract class AsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey>
        : AsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, PagedAndSortedResultRequestDto>
        where TEntity : class, IEntity<TPrimaryKey>
        where TEntityDto : IEntityDto<TPrimaryKey>
    {
        protected AsyncCrudAppService(IRepository<TEntity, TPrimaryKey> repository)
            : base(repository)
        {

        }
    }

    public abstract class AsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput>
        : AsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TEntityDto, TEntityDto>
        where TEntity : class, IEntity<TPrimaryKey>
        where TEntityDto : IEntityDto<TPrimaryKey>
    {
        protected AsyncCrudAppService(IRepository<TEntity, TPrimaryKey> repository)
            : base(repository)
        {

        }
    }

    public abstract class AsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput>
        : AsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TCreateInput>
        where TGetAllInput : IPagedAndSortedResultRequest
        where TEntity : class, IEntity<TPrimaryKey>
        where TEntityDto : IEntityDto<TPrimaryKey>
       where TCreateInput : IEntityDto<TPrimaryKey>
    {
        protected AsyncCrudAppService(IRepository<TEntity, TPrimaryKey> repository)
            : base(repository)
        {

        }
    }

    public abstract class AsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput>
        : AsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, EntityDto<TPrimaryKey>>
        where TEntity : class, IEntity<TPrimaryKey>
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TUpdateInput : IEntityDto<TPrimaryKey>
    {
        protected AsyncCrudAppService(IRepository<TEntity, TPrimaryKey> repository)
            : base(repository)
        {

        }
    }

    public abstract class AsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, TGetInput>
    : AsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, TGetInput, EntityDto<TPrimaryKey>>
        where TEntity : class, IEntity<TPrimaryKey>
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TUpdateInput : IEntityDto<TPrimaryKey>
        where TGetInput : IEntityDto<TPrimaryKey>
    {
        protected AsyncCrudAppService(IRepository<TEntity, TPrimaryKey> repository)
            : base(repository)
        {

        }
    }
    public abstract class AsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, TGetInput, TDeleteInput>
       : CrudAppServiceBase<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput>,
        IAsyncCrudAppService<TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, TGetInput, TDeleteInput>
           where TEntity : class, IEntity<TPrimaryKey>
           where TEntityDto : IEntityDto<TPrimaryKey>
           where TUpdateInput : IEntityDto<TPrimaryKey>
           where TGetInput : IEntityDto<TPrimaryKey>
           where TDeleteInput : IEntityDto<TPrimaryKey>
    {
        #region 声明实例
        public IAsyncQueryableExecuter AsyncQueryableExecuter { get; set; }
        #endregion
        #region 构造函数
        protected AsyncCrudAppService(IRepository<TEntity, TPrimaryKey> repository)
            : base(repository)
        {
            AsyncQueryableExecuter = NullAsyncQueryableExecuter.Instance;
        }
        #endregion
        #region 方法

        
        public virtual async Task<TEntityDto> Create(TCreateInput input)
        {
            var entity= MapToEntity(input);
            await Repository.InsertAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();
            return MapToEntityDto(entity);
        }

        public virtual Task Delete(TDeleteInput input)
        {
            return Repository.DeleteAsync(input.Id);
        }

        public virtual async Task<TEntityDto> Get(TGetInput input)
        {
            var entity = await GetEntityByIdAsync(input.Id);
            var temp= MapToEntityDto(entity);
            return MapToEntityDto(entity);
        }

        public virtual async Task<PagedResultDto<TEntityDto>> GetAllAsync(TGetAllInput input)
        {
            var query= CreateFilteredQuery(input);
            var totalCount = await AsyncQueryableExecuter.CountAsync(query);
            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);
            var entities = await AsyncQueryableExecuter.ToListAsync(query);
            return new PagedResultDto<TEntityDto>(

                totalCount,
                entities.Select(MapToEntityDto).ToList()
                );
        }

        public virtual async Task<TEntityDto> Update(TUpdateInput input)
        {
            var entity = await GetEntityByIdAsync(input.Id);
            MapToEntity(input, entity);
            await CurrentUnitOfWork.SaveChangesAsync();
            return MapToEntityDto(entity);
        }
        protected virtual Task<TEntity> GetEntityByIdAsync(TPrimaryKey id)
        {
            return Repository.GetAsync(id);
        }
        #endregion
    }
}
