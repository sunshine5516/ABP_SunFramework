using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace AbpFramework.Domain.Entities.Auditing
{
    [Serializable]
    public abstract class AuditedEntity : AuditedEntity<int>, IEntity
    {

    }
    /// <summary>
    /// 支持主键是泛型类型的Entity,并且从其父类接口那继承了Creation 和 Modification 的时间和UserID，这个是long类型
    /// </summary>
    /// <typeparam name="TPrimaryKey">实体主键类型</typeparam>
    [Serializable]
    public abstract class AuditedEntity<TPrimaryKey> : CreationAuditedEntity<TPrimaryKey>, IAudited
    {
        /// <summary>
        /// 最后修改的用户的ID
        /// </summary>
        public virtual long? LastModifierUserId { get; set; }
        /// <summary>
        /// 最后修改的时间
        /// </summary>
        public virtual DateTime? LastModificationTime { get; set; }
    }
    public abstract class AuditedEntity<TPrimaryKey, TUser> : AuditedEntity<TPrimaryKey>, IAudited<TUser>
        where TUser : IEntity<long>
    {
        /// <summary>
        /// 创建者
        /// </summary>
        [ForeignKey("CreatorUserId")]
        public virtual TUser CreatorUser { get; set; }

        /// <summary>
        /// 最后修改的用户
        /// </summary>
        [ForeignKey("LastModifierUserId")]
        public virtual TUser LastModifierUser { get; set; }
    }
}
