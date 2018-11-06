using AbpFramework.Dependency;
using Newtonsoft.Json;
namespace AbpFramework.Auditing
{
    public class JsonNetAuditSerializer : IAuditSerializer, ITransientDependency
    {
        #region 声明实例
        private readonly IAuditingConfiguration _configuration;
        #endregion
        #region 构造函数
        public JsonNetAuditSerializer(IAuditingConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion
        #region 方法
        public string Serialize(object obj)
        {
            var options = new JsonSerializerSettings
            {
                ContractResolver = new AuditingContractResolver(_configuration.IgnoredTypes)
            };
            return JsonConvert.SerializeObject(obj, options);
        }
        #endregion

    }
}
