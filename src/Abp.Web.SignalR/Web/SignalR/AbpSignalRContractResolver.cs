using Microsoft.AspNet.SignalR.Infrastructure;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Reflection;
namespace Abp.Web.SignalR.Web.SignalR
{
    public class AbpSignalRContractResolver : IContractResolver
    {
        #region 声明实例
        public static List<Assembly> IgnoredAssemblies { get; private set; }
        private readonly IContractResolver _camelCaseContractResolver;
        private readonly IContractResolver _defaultContractSerializer;
        #endregion
        #region 构造函数
        static AbpSignalRContractResolver()
        {
            IgnoredAssemblies = new List<Assembly>
            {
                typeof (Connection).Assembly
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbpSignalRContractResolver"/> class.
        /// </summary>
        public AbpSignalRContractResolver()
        {
            _defaultContractSerializer = new DefaultContractResolver();
            _camelCaseContractResolver = new CamelCasePropertyNamesContractResolver();
        }
        #endregion
        #region 方法

        #endregion
        public JsonContract ResolveContract(Type type)
        {
            if (IgnoredAssemblies.Contains(type.Assembly))
            {
                return _defaultContractSerializer.ResolveContract(type);
            }
            return _camelCaseContractResolver.ResolveContract(type);
        }
    }
}
