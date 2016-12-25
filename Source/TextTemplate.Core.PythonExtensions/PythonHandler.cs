using System.Collections.Generic;
using System.Text;
using TextTemplate.Core.PythonExtensions.PyExtensions;
using TextTemplate.Core.PythonExtensions.PyExtensions.Base;
using TextTemplate.Infrastructure;

namespace TextTemplate.Core.PythonExtensions
{
    public class PythonHandler : ITemplateHandler
    {
        private readonly IPyHelper[] _helpers = {new IfExtension(), new ElifExtension(), };
        public StringBuilder ChangeTemplate(StringBuilder template, IDictionary<string,object> parameters = null)
        {
            foreach (var pyHelper in _helpers)
                pyHelper.Handle(template, parameters);
            return template;
        }
    }
}
