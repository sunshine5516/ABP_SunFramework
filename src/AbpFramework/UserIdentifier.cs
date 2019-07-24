using AbpFramework.Extensions;
using System;
using System.Reflection;
namespace AbpFramework
{
    /// <summary>
    /// 用户标识.
    /// </summary>
    [Serializable]
    public class UserIdentifier : IUserIdentifier
    {
        #region 声明实例
        public int? TenantId { get; protected set; }

        public long UserId { get; protected set; }
        #endregion
        #region 构造函数
        protected UserIdentifier()
        {

        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tenantId">租户ID.</param>
        /// <param name="userId">用户ID.</param>
        public UserIdentifier(int? tenantId, long userId)
        {
            TenantId = tenantId;
            UserId = userId;
        }
        #endregion
        #region 方法
        public static UserIdentifier Parse(string userIdentifierString)
        {
            if (userIdentifierString.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(userIdentifierString), "userAtTenant can not be null or empty!");
            }
            var splitted = userIdentifierString.Split('@');
            if(splitted.Length==1)
            {
                return new UserIdentifier(null, splitted[0].To<long>());
            }
            if (splitted.Length == 2)
            {
                return new UserIdentifier(splitted[1].To<int>(), splitted[0].To<long>());
            }

            throw new ArgumentException("userAtTenant is not properly formatted", nameof(userIdentifierString));
        }
        /// <summary>
        /// 创建一个字符串表示此<see cref ="UserIdentifier"/>实例。
        /// </summary>
        /// <returns></returns>
        public string ToUserIdentifierString()
        {
            if(TenantId==null)
            {
                return UserId.ToString();
            }
            return UserId + "@" + TenantId;
        }
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is UserIdentifier))
            {
                return false;
            }

            //Same instances must be considered as equal
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            //Transient objects are not considered as equal
            var other = (UserIdentifier)obj;

            //Must have a IS-A relation of types or must be same type
            var typeOfThis = GetType();
            var typeOfOther = other.GetType();
            if (!typeOfThis.GetTypeInfo().IsAssignableFrom(typeOfOther) && !typeOfOther.GetTypeInfo().IsAssignableFrom(typeOfThis))
            {
                return false;
            }

            return TenantId == other.TenantId && UserId == other.UserId;
        }
        public override int GetHashCode()
        {
            return TenantId==null?(int)UserId:(int)(TenantId.Value^UserId);
        }/// <inheritdoc/>
        public static bool operator ==(UserIdentifier left, UserIdentifier right)
        {
            if (Equals(left, null))
            {
                return Equals(right, null);
            }

            return left.Equals(right);
        }

        /// <inheritdoc/>
        public static bool operator !=(UserIdentifier left, UserIdentifier right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return ToUserIdentifierString();
        }
        #endregion
    }
}
