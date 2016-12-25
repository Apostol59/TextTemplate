using System.Collections.Generic;
using System.Text;

namespace TextTemplate.Infrastructure
{
    public interface ITemplateHandler
    {
        StringBuilder ChangeTemplate(StringBuilder template, IDictionary<string, object> parameters = null);
    }
}