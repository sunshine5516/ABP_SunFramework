using System;
namespace AbpFramework.Application.Services.Dto
{
    [Serializable]
    public class EntityDto : EntityDto<int>, IEntityDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public EntityDto()
        {

        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">Id of the entity</param>
        public EntityDto(int id)
            : base(id)
        {
        }
    }
    [Serializable]
    public class EntityDto<TPrimaryKey> : IEntityDto<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        public EntityDto()
        {

        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">Id of the entity</param>
        public EntityDto(TPrimaryKey id)
        {
            Id = id;
        }
    }
}
