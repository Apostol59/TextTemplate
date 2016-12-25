using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using TextTemplate.Core.PythonExtensions.PyExtensions;
using TextTemplate.Core.PythonExtensions.PyExtensions.Base;

namespace TextTemplate.Core.PythonExtensions.Tests
{
    public class PyExtensionsTests
    {
        
        [TestCase(@"{{#py.if ""a"" == ""a""}}", ExpectedResult = @"{{#if ""__true__""}}")]
        [TestCase(@"{{#py.if ""a"" == ""b""}}", ExpectedResult = @"{{#if __false__}}")]
        public string IfExtension(string template)
        {
            IPyHelper pyExt = new IfExtension();
            var result = pyExt.Handle(new StringBuilder(template));
            return result.ToString();
        }


        [TestCase(@"{{#py.if ""a"" == ""a""}}", ExpectedResult = @"{{#py.if ""a"" == ""a""}}")]
        [TestCase(@"{{#py.if ""a"" == ""b""}}", ExpectedResult = @"{{#py.if ""a"" == ""b""}}")]
        [TestCase(@"{{#py.elif ""a"" == ""a""}}", ExpectedResult = @"{{else if ""__true__""}}")]
        [TestCase(@"{{#py.elif ""a"" == ""b""}}", ExpectedResult = @"{{else if __false__}}")]
        public string ElifExtension(string template)
        {
            IPyHelper pyExt = new ElifExtension();
            var result = pyExt.Handle(new StringBuilder(template));
            return result.ToString();
        }

        [TestCase(@"{{#py.if a == a}}", ExpectedResult = @"{{#if ""__true__""}}")]
        [TestCase(@"{{#py.if a in (""NameA"",""NameB"",""NameC"")}}", ExpectedResult = @"{{#if ""__true__""}}")]
        [TestCase(@"{{#py.if a == b}}", ExpectedResult = @"{{#if __false__}}")]
        public string IfExtensionWithParameters(string template)
        {
            IPyHelper pyExt = new IfExtension();
            var parameters = new Dictionary<string, object>()
            {
                { "a", "NameA"},
                { "b", "NameB"},
            };
            var result = pyExt.Handle(new StringBuilder(template), parameters);
            return result.ToString();
        }
    }
}
