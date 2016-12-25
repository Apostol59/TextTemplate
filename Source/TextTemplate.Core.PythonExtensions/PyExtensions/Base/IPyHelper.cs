using System.Collections.Generic;
using System.Text;

namespace TextTemplate.Core.PythonExtensions.PyExtensions.Base
{
    public interface IPyHelper
    {
        StringBuilder Handle(StringBuilder template, IDictionary<string, object> parameters = null);
    }
}