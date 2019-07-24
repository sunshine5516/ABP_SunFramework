using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
namespace Abp.WebApi.Controllers.Dynamic.Formatters
{
    /// <summary>
    /// 用于返回纯文本响应 <see cref="ApiController"/>s.
    /// 自定义的针对"text/plain"的媒体格式化器。
    /// 服务器端通过WebApi返回给客户端的Javascript脚本时所使用的媒体格式化器。
    /// </summary>
    public class PlainTextFormatter : MediaTypeFormatter
    {
        /// <summary>
        /// 构造函数.
        /// </summary>
        public PlainTextFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/plain"));
        }
        public override bool CanReadType(Type type)
        {
            return type == typeof(string);
        }

        public override bool CanWriteType(Type type)
        {
            return type == typeof(string);
        }
        public override Task WriteToStreamAsync
            (Type type, object value, Stream stream, HttpContent content, 
            TransportContext transportContext)
        {
            using (var writer = new StreamWriter(stream))
            {
                writer.Write((string)value);
                writer.Flush();
            }

            var tcs = new TaskCompletionSource<object>();
            tcs.SetResult(null);
            return tcs.Task;
        }
        public override Task<object> ReadFromStreamAsync
          (Type type, Stream stream, HttpContent content, IFormatterLogger formatterLogger)
        {
            string value;
            using (var reader = new StreamReader(stream))
            {
                value = reader.ReadToEnd();
            }

            var tcs = new TaskCompletionSource<object>();
            tcs.SetResult(value);
            return tcs.Task;
        }
    }
}
