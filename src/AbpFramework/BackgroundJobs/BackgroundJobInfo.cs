using AbpFramework.Domain.Entities.Auditing;
using AbpFramework.MultiTenancy;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AbpFramework.BackgroundJobs
{
    /// <summary>
    /// 用于持久化job信息的实体类，对应于数据库中的表AbpBackgroundJobs   
    /// </summary>
    [Table("AbpBackgroundJobs")]
    [MultiTenancySide(MultiTenancySides.Host)]
    public class BackgroundJobInfo: CreationAuditedEntity<long>
    {
        /// <summary>
        /// 最大长度
        /// Value: 512.
        /// </summary>
        public const int MaxJobTypeLength = 512;
        /// <summary>
        /// 参数最大长度
        /// Value: 1 MB (1,048,576 bytes).
        /// </summary>
        public const int MaxJobArgsLength = 1024 * 1024;

        /// <summary>
        /// 默认持续时间（以秒为单位）第一次等待失败。
        /// 默认值: 60 (1 minutes).
        /// </summary>
        public static int DefaultFirstWaitDuration { get; set; }

        /// <summary>
        /// 作业放弃之前的默认超时值（以秒为单位）（<see cref ="IsAbandoned"/>）。       
        /// Default value: 172,800 (2 days).
        /// </summary>
        public static int DefaultTimeout { get; set; }

        /// <summary>
        /// Default wait factor for execution failures.
        /// 默认值: 2.0.
        /// </summary>
        public static double DefaultWaitFactor { get; set; }

        /// <summary>
        /// 类型
        /// It's AssemblyQualifiedName of job type.
        /// </summary>
        [Required]
        [StringLength(MaxJobTypeLength)]
        public virtual string JobType { get; set; }

        /// <summary>
        /// JSON字符串作业参数.
        /// </summary>
        [Required]
        [MaxLength(MaxJobArgsLength)]
        public virtual string JobArgs { get; set; }

        /// <summary>
        /// 作业的尝试次数
        /// 如果失败，工作将被重新执行。
        /// </summary>
        public virtual short TryCount { get; set; }

        /// <summary>
        /// 接下来尝试这个工作的时间。
        /// </summary>
        //[Index("IX_IsAbandoned_NextTryTime", 2)]
        public virtual DateTime NextTryTime { get; set; }

        /// <summary>
        /// 最后执行的时间
        /// </summary>
        public virtual DateTime? LastTryTime { get; set; }

        /// <summary>
        /// true:这个工作连续失败并且不会被再次执行
        /// </summary>
        //[Index("IX_IsAbandoned_NextTryTime", 1)]
        public virtual bool IsAbandoned { get; set; }

        /// <summary>
        /// 工作的优先顺序。
        /// </summary>
        public virtual BackgroundJobPriority Priority { get; set; }
        static BackgroundJobInfo()
        {
            DefaultFirstWaitDuration = 60;
            DefaultTimeout = 172800;
            DefaultWaitFactor = 2.0;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public BackgroundJobInfo()
        {
            NextTryTime = DateTime.Now;
            Priority = BackgroundJobPriority.Normal;
        }
        /// <summary>
        /// 计算下一次尝试的时间，如果一个工作失败。
        /// 如果不再等待，则返回null，应放弃作业。
        /// </summary>
        /// <returns></returns>
        public virtual DateTime? CalculateNextTryTime()
        {
            var nextWatiDuration=DefaultFirstWaitDuration* (Math.Pow(DefaultWaitFactor, TryCount - 1));
            var nextTryDate = LastTryTime.HasValue
                ? LastTryTime.Value.AddSeconds(nextWatiDuration)
                : DateTime.Now.AddSeconds(nextWatiDuration);
            if (nextTryDate.Subtract(CreationTime).TotalSeconds > DefaultTimeout)
            {
                return null;
            }

            return nextTryDate;
        }
    }
}
