using AbpFramework.Application.Services.Dto;
using AbpFramework.Domain.Entities;
using AbpFramework.Domain.Repositories;
using System.Linq;
namespace AbpFramework.Application.Services
{
    public abstract class CrudAppService<TEntity, TEntityDto>
        : CrudAppService<TEntity, TEntityDto, int>
        where TEntity : class, IEntity<int>
        where TEntityDto : IEntityDto<int>
    {
        protected CrudAppService(IRepository<TEntity, int> repository)
            : base(repository)
        {

        }
    }
    public abstract class CrudAppService<TEntity, TEntityDto, TPrimaryKey>
         : CrudAppService<TEntity, TEntityDto, TPrimaryKey, PagedAndSortedResultRequestDto>
        where TEntity : class, IEntity<TPrimaryKey>
        where TEntityDto : IEntityDto<TPrimaryKey>
    {
        protected CrudAppService(IRepository<TEntity, TPrimaryKey> repository)
            : base(repository)
        {

        }
    }
    public abstract class CrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput>
        : CrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TEntityDto, TEntityDto>
        where TEntity : class, IEntity<TPrimaryKey>
        where TEntityDto : IEntityDto<TPrimaryKey>
    {
        protected CrudAppService(IRepository<TEntity, TPrimaryKey> repository)
            : base(repository)
        {

        }
    }
    public abstract class CrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput>
        : CrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, EntityDto<TPrimaryKey>>
        where TEntity : class, IEntity<TPrimaryKey>
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TUpdateInput : IEntityDto<TPrimaryKey>
    {
        protected CrudAppService(IRepository<TEntity, TPrimaryKey> repository)
            : base(repository)
        {

        }
    }
    public abstract class CrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, TGetInput>
   : CrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, TGetInput, EntityDto<TPrimaryKey>>
       where TEntity : class, IEntity<TPrimaryKey>
       where TEntityDto : IEntityDto<TPrimaryKey>
       where TUpdateInput : IEntityDto<TPrimaryKey>
       where TGetInput : IEntityDto<TPrimaryKey>
    {
        protected CrudAppService(IRepository<TEntity, TPrimaryKey> repository)
            : base(repository)
        {

        }
    }
    public abstract class CrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, TGetInput, TDeleteInput>
      : CrudAppServiceBase<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput>,
       ICrudAppService<TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, TGetInput, TDeleteInput>
          where TEntity : class, IEntity<TPrimaryKey>
          where TEntityDto : IEntityDto<TPrimaryKey>
          where TUpdateInput : IEntityDto<TPrimaryKey>
          where TGetInput : IEntityDto<TPrimaryKey>
          where TDeleteInput : IEntityDto<TPrimaryKey>
    {
        protected CrudAppService(IRepository<TEntity, TPrimaryKey> repository)
            :base(repository)
        {

        }
        public TEntityDto Get(TGetInput input)
        {
            var entity = GetEntityById(input.Id);
            return MapToEntityDto(entity);
        }

        public PagedResultDto<TEntityDto> GetAll(TGetAllInput input)
        {
            var query = CreateFilteredQuery(input);
            var totalCount = query.Count();
            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);
            var entities = query.ToList();
            return new PagedResultDto<TEntityDto>(
                totalCount,
                entities.Select(MapToEntityDto).ToList());
        }
        public TEntityDto Create(TCreateInput input)
        {
            var entity = MapToEntity(input);
            Repository.Insert(entity);
            CurrentUnitOfWork.SaveChanges();
            return MapToEntityDto(entity);
        }

        public void Delete(TDeleteInput input)
        {
            Repository.Delete(input.Id);
        }

       

        public TEntityDto Update(TUpdateInput input)
        {
            var entity = GetEntityById(input.Id);

            MapToEntity(input, entity);
            CurrentUnitOfWork.SaveChanges();

            return MapToEntityDto(entity);
        }
        protected virtual TEntity GetEntityById(TPrimaryKey id)
        {
            return Repository.Get(id);
        }
    }
}
