using System.Collections.Generic;
using NUnit.Framework;

namespace TextTemplate.Core.Tests
{
    public class HbsWrapperWrapperTests : HbsWrapperTemplate
    {
        [TestCase("{{someStr}}", ExpectedResult = "WS")]
        [TestCase("{{root.NotFound}}", ExpectedResult = "")]
        [TestCase("{{root.Name}}", ExpectedResult = "Dan")]
        public string ParameterTests(string source)
        {
            var parameters = new Dictionary<string, object>
            {
                {"someStr", "WS" },
                {"root", new {Name="Dan"} }
            };
            
           return this.RenderTemplate(source, parameters);
        }

        [TestCase("Hi, {{>somePart}} =)", ExpectedResult = "Hi, Endy =)")]
        public string PartialsTest(string source)
        {
            var parametrs = new Dictionary<string, object>
            {
                { "root", new {Name="Endy", Age=20}}
            };
            return this.RenderTemplate(source, parametrs, new Dictionary<string, string>()
            {
                {"somePart", "{{root.Name}}" }
            });
        }

        [TestCase(@"Hi, {{#py.if ""Nice"" in [""cool"",""Nice""]}}you are nice){{/if}}", ExpectedResult = "Hi, you are nice)")]
        [TestCase(@"Hi, {{#py.if ""Nice"" in [""cool"",""nice""]}} oh( {{#py.elif ""true""==""true""}}you are nice){{/if}}", ExpectedResult = "Hi, you are nice)")]
        public string WithPyHelper(string source)
        {
            return this.RenderTemplate(source);
        }
    }
}
