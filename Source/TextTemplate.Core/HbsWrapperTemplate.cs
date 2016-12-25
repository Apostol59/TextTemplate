using System.Collections.Generic;
using System.IO;
using System.Text;
using HandlebarsDotNet;
using TextTemplate.Core.PythonExtensions;
using TextTemplate.Infrastructure;

namespace TextTemplate.Core
{
    public class HbsWrapperTemplate : IHbsWrapperTemplate
    {
        private readonly ITemplateHandler[] _handlers = {new PythonHandler()};

        public string RenderTemplate(string templateSource,
            IDictionary<string, object> parameters = null,
            IDictionary<string, string> partialSource = null)
        {
            if (parameters == null)
                parameters = new Dictionary<string, object>();
            var template = Handlebars.Create();
            if ((partialSource != null) && (partialSource.Count > 0))
                foreach (var keyValuePair in partialSource)
                    using (var reader = new StringReader(ChangeTemplate(keyValuePair.Value, parameters)))
                    {
                        var partialTemplate = template.Compile(reader);
                        template.RegisterTemplate(keyValuePair.Key, partialTemplate);
                    }
            var compile = template.Compile(ChangeTemplate(templateSource, parameters));
            var result = compile(parameters);
            return result;
        }

        private string ChangeTemplate(string template, IDictionary<string, object> parameters)
        {
            var templateSb = new StringBuilder(template);
            foreach (var templateHandler in _handlers)
                templateHandler.ChangeTemplate(templateSb, parameters);
            return templateSb.ToString();
        }
    }
}