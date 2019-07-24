using System.Collections.Generic;
namespace Abp.Web.Common.Web.Api.ProxyScripting.Generators
{
    public static class ProxyScriptingJsFuncHelper
    {
        private const string ValidJsVariableNameChars = "abcdefghijklmnopqrstuxwvyzABCDEFGHIJKLMNOPQRSTUXWVYZ0123456789_";

        private static readonly HashSet<string> ReservedWords = new HashSet<string> {
            "abstract",
            "else",
            "instanceof",
            "super",
            "boolean",
            "enum",
            "int",
            "switch",
            "break",
            "export",
            "interface",
            "synchronized",
            "byte",
            "extends",
            "let",
            "this",
            "case",
            "false",
            "long",
            "throw",
            "catch",
            "final",
            "native",
            "throws",
            "char",
            "finally",
            "new",
            "transient",
            "class",
            "float",
            "null",
            "true",
            "const",
            "for",
            "package",
            "try",
            "continue",
            "function",
            "private",
            "typeof",
            "debugger",
            "goto",
            "protected",
            "var",
            "default",
            "if",
            "public",
            "void",
            "delete",
            "implements",
            "return",
            "volatile",
            "do",
            "import",
            "short",
            "while",
            "double",
            "in",
            "static",
            "with"
        };
        public static string WrapWithBracketsOrWithDotPrefix(string name)
        {
            if(!ReservedWords.Contains(name))
            {
                return "." + name;
            }
            return "['" + name + "']";
        }
    }
}
