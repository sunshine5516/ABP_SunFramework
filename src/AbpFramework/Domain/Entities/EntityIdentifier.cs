using System;
namespace AbpFramework.Domain.Entities
{
    /// <summary>
    /// 用于标识实体
    /// </summary>
    [Serializable]
    public class EntityIdentifier
    {
        /// <summary>
        /// Entity Type.
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// Entity's Id.
        /// </summary>
        public object Id { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        private EntityIdentifier()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type">Entity type.</param>
        /// <param name="id">Id of the entity.</param>
        public EntityIdentifier(Type type, object id)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            Type = type;
            Id = id;
        }
    }
}
