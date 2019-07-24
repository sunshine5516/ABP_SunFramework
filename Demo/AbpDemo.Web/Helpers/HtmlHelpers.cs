using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AbpDemo.Web.Helpers
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString PartialViewScripts(this HtmlHelper helper)
        {
            return MvcHtmlString.Create(string.Join(Environment.NewLine, ScriptBlock.PartialViewScripts.Select(s => s.ToString())));
        }
        private class ScriptBlock : IDisposable
        {
            private const string ScriptsKey = "PartialViewScripts";
            public static List<string> PartialViewScripts
            {
                get
                {
                    if (HttpContext.Current.Items[ScriptsKey] == null)
                        HttpContext.Current.Items[ScriptsKey] = new List<string>();
                    return (List<string>)HttpContext.Current.Items[ScriptsKey];
                }
            }

            readonly WebViewPage _webPageBase;

            public ScriptBlock(WebViewPage webPageBase)
            {
                _webPageBase = webPageBase;
                _webPageBase.OutputStack.Push(new StringWriter());
            }

            public void Dispose()
            {
                PartialViewScripts.Add(((StringWriter)this._webPageBase.OutputStack.Pop()).ToString());
            }
        }
    }
}