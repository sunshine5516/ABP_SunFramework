using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Json
{
    /// <summary>
    /// JSON帮助类.
    /// </summary>
    public static class JsonSerializationHelper
    {
        private const char TypeSeperator = '|';

        /// <summary>
        /// 序列化对象
        /// </summary>
        public static string SerializeWithType(object obj)
        {
            return SerializeWithType(obj, obj.GetType());
        }

        /// <summary>
        /// 序列化对象
        /// </summary>
        public static string SerializeWithType(object obj, Type type)
        {
            //var serialized = obj.ToJsonString();

            //return string.Format(
            //    "{0}{1}{2}",
            //    type.AssemblyQualifiedName,
            //    TypeSeperator,
            //    serialized
            //    );
            return "";
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        public static T DeserializeWithType<T>(string serializedObj)
        {
            return (T)DeserializeWithType(serializedObj);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        public static object DeserializeWithType(string serializedObj)
        {
            var typeSeperatorIndex = serializedObj.IndexOf(TypeSeperator);
            var type = Type.GetType(serializedObj.Substring(0, typeSeperatorIndex));
            var serialized = serializedObj.Substring(typeSeperatorIndex + 1);

            var options = new JsonSerializerSettings();
            //options.Converters.Insert(0, new AbpDateTimeConverter());

            return JsonConvert.DeserializeObject(serialized, type, options);
        }
    }
}
