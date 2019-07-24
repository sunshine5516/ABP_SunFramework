using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace AbpFramework.Domain.Entities.Auditing
{
    /// <summary>
    ///  (<see cref="int"/>)主键的快捷实现.
    /// </summary>
    [Serializable]
    public abstract class FullAuditedEntity : FullAuditedEntity<int>, IEntity
    {

    }
    /// <summary>
    /// 实现<see cref="IFullAudited"/>作为完全审核实体的基类。
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public class FullAuditedEntity<TPrimaryKey> : AuditedEntity<TPrimaryKey>, IFullAudited
    {
        /// <summary>
        /// 删除实体的用户的ID
        /// </summary>
        public virtual long? DeleterUserId { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        public virtual DateTime? DeletionTime { get; set; }
        /// <summary>
        /// 是否被删除
        /// </summary>
        public virtual bool IsDeleted { get; set; }
    }
    /// <summary>
    /// 实现<see cref="IFullAudited"/>作为完全审核实体的基类.
    /// </summary>
    /// <typeparam name="TPrimaryKey">实体主键类型</typeparam>
    /// <typeparam name="TUser">用户类型</typeparam>
    public abstract class FullAuditedEntity<TPrimaryKey, TUser> : AuditedEntity<TPrimaryKey, TUser>, IFullAudited<TUser>
        where TUser : IEntity<long>
    {
        /// <summary>
        /// 是否被删除
        /// </summary>
        public virtual bool IsDeleted { get; set; }

        /// <summary>
        /// 删除该实体的用户.
        /// </summary>
        [ForeignKey("DeleterUserId")]
        public virtual TUser DeleterUser { get; set; }

        /// <summary>
        /// 删除实体的用户的ID
        /// </summary>
        public virtual long? DeleterUserId { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public virtual DateTime? DeletionTime { get; set; }
    }
}
