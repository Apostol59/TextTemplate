using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using TextTemplate.Infrastructure;

namespace TextTemplate.Core.PythonExtensions.Tests
{
    public class PythonHandlerTest
    {
        
        [TestCase("", ExpectedResult = "")]
        [TestCase("text without py", ExpectedResult = "text without py")]
        [TestCase("text without py {{#if __wtf__}}", ExpectedResult = "text without py {{#if __wtf__}}")]
        [TestCase("text without py helper {{#py.wtf}}", ExpectedResult = "text without py helper {{#py.wtf}}")]
        public string CleanText(string template)
        {
            ITemplateHandler handler = new PythonHandler();
            var templateSb = new StringBuilder(template);

            handler.ChangeTemplate(templateSb);
            return templateSb.ToString();
        }

        [TestCase(@"{{#py.if ""str1"" in (""str1"",""str2"",""str3"")}}", ExpectedResult = @"{{#if ""__true__""}}")]
        [TestCase(@"{{#py.if ""str2"" in {""str1"",""str2"",""str3""} }}", ExpectedResult = @"{{#if ""__true__""}}")]
        [TestCase(@"{{#py.if ""str3"" in [""str1"",""str2"",""str3""]}}", ExpectedResult = @"{{#if ""__true__""}}")]
        [TestCase(@"{{#py.if ""str4"" in [""str1"",""str2"",""str3""]}}", ExpectedResult = @"{{#if __false__}}")]
        [TestCase(@"{{#py.elif ""str3"" in [""str1"",""str2"",""str3""]}}", ExpectedResult = @"{{else if ""__true__""}}")]
        [TestCase(@"{{#py.elif ""str4"" in [""str1"",""str2"",""str3""]}}", ExpectedResult = @"{{else if __false__}}")]
        public string SomePyHelper(string template)
        {
            ITemplateHandler handler = new PythonHandler();
            var templateSb = new StringBuilder(template);

            handler.ChangeTemplate(templateSb);
            return templateSb.ToString();
        }


        [TestCase(@"{{#py.if str in (""str1"",""str2"",""str3"")}}", "str1",ExpectedResult = @"{{#if ""__true__""}}")]
        [TestCase(@"{{#py.if str in {""str1"",""str2"",""str3""} }}","str2", ExpectedResult = @"{{#if ""__true__""}}")]
        [TestCase(@"{{#py.if str in [""str1"",""str2"",""str3""]}}", "str3",ExpectedResult = @"{{#if ""__true__""}}")]
        [TestCase(@"{{#py.if str in [""str1"",""str2"",""str3""]}}", "str4",ExpectedResult = @"{{#if __false__}}")]
        [TestCase(@"{{#py.elif str in [""str1"",""str2"",""str3""]}}","str3", ExpectedResult = @"{{else if ""__true__""}}")]
        [TestCase(@"{{#py.elif str in [""str1"",""str2"",""str3""]}}","str4", ExpectedResult = @"{{else if __false__}}")]
        public string SomePyHelperWithParameters(string template, string parameter)
        {
            ITemplateHandler handler = new PythonHandler();
            var parameters = new Dictionary<string, object>()
            {
                {"str", parameter }
            };
            var templateSb = new StringBuilder(template);

            handler.ChangeTemplate(templateSb, parameters);
            return templateSb.ToString();
        }


    }
}
