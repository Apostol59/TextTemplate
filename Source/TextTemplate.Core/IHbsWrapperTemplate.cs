using System.Collections.Generic;

namespace TextTemplate.Core
{
    public interface IHbsWrapperTemplate
    {
        string RenderTemplate(string templateSource, IDictionary<string, object> parameters = null, IDictionary<string, string> partialSource = null);
    }
}