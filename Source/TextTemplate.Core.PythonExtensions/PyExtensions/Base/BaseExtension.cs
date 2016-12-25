using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace TextTemplate.Core.PythonExtensions.PyExtensions.Base
{
    public abstract class BaseExtension<TResultExtension> : IPyHelper
    {
        private const string PyExtStart = "{{#py.";
        private const string PyExtEnd = "}}";
        private readonly Regex _helperRegex = new Regex(@"\{\{#py\..+? +?.+?\}\}");
        private readonly ScriptEngine _scriptEngine = Python.CreateEngine();
        protected abstract string HelperName { get; }

        protected Match[] FindExtensions(StringBuilder template)
        {
            var mathes = _helperRegex.Matches(template.ToString());
            return (from Match match in mathes
                where match.Value.StartsWith(PyExtStart + HelperName)
                select match).ToArray();
        }

        public StringBuilder Handle(StringBuilder template, IDictionary<string, object> parameters = null)
        {
            var findExtensions = FindExtensions(template);
            if (findExtensions.Length == 0)
                return template;
            if(parameters == null)
                parameters = new Dictionary<string, object>();

            foreach (var match in findExtensions)
            {
                var scope = _scriptEngine.CreateScope(parameters);
                var script = match.Value.Substring(PyExtStart.Length + HelperName.Length,
                    match.Value.Length - (PyExtStart.Length + HelperName.Length) - PyExtEnd.Length).Trim();
                var scriptResult = _scriptEngine.Execute<TResultExtension>(script, scope);
                var replaceResult = ReplaceResult(scriptResult, scope);
                template.Replace(match.Value, replaceResult);
            }
            return template;
        }

        protected abstract string ReplaceResult(TResultExtension scriptResult, ScriptScope scope);
    }
}