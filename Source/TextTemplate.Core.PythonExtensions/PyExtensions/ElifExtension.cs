using Microsoft.Scripting.Hosting;
using TextTemplate.Core.PythonExtensions.PyExtensions.Base;

namespace TextTemplate.Core.PythonExtensions.PyExtensions
{
    public class ElifExtension : BaseExtension<bool>
    {
        protected override string HelperName { get { return "elif"; } }
        protected override string ReplaceResult(bool scriptResult, ScriptScope scope)
        {
            return scriptResult ? @"{{else if ""__true__""}}" : @"{{else if __false__}}";
        }
    }
}
