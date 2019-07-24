using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
namespace AbpFramework.Json
{
    public static class JsonExtensions
    {
        public static string ToJsonString(this object obj, bool camelCase = false, bool indented = false)
        {
            var options = new JsonSerializerSettings();
            if (camelCase)
            {
                options.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }
            if (indented)
            {
                options.Formatting = Formatting.Indented;
            }
            //options.Converters.Insert(0, new AbpDateTimeConverter());
            options.Converters.Insert(0, new IsoDateTimeConverter());
            return JsonConvert.SerializeObject(obj, options);
        }
    }
}
