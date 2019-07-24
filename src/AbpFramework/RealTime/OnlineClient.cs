using AbpFramework.Json;
using System;
using System.Collections.Generic;
namespace AbpFramework.RealTime
{
    [Serializable]
    public class OnlineClient : IOnlineClient
    {
        public object this[string key]
        {
            get { return Properties[key]; }
            set { Properties[key] = value; }
        }

        public string ConnectionId { get; set; }

        public string IpAddress { get; set; }

        public int? TenantId { get; set; }

        public long? UserId { get; set; }

        public DateTime ConnectTime { get; set; }
        private Dictionary<string, object> _properties;

        public Dictionary<string, object> Properties
        {
            get { return _properties; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                _properties = value;
            }
        }
        #region 构造函数
        public OnlineClient()
        {
            ConnectTime = DateTime.Now;
        }
        public OnlineClient(string connectionId, string ipAddress, int? tenantId, long? userId)
          : this()
        {
            ConnectionId = connectionId;
            IpAddress = ipAddress;
            TenantId = tenantId;
            UserId = userId;

            Properties = new Dictionary<string, object>();
        }

        public override string ToString()
        {
            return this.ToJsonString();
        }
        #endregion
    }
}
