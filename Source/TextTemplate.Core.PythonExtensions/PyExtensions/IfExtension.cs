using Microsoft.Scripting.Hosting;
using TextTemplate.Core.PythonExtensions.PyExtensions.Base;

namespace TextTemplate.Core.PythonExtensions.PyExtensions
{
    public class IfExtension : BaseExtension<bool>
    {
        protected override string HelperName { get { return "if"; } }
        protected override string ReplaceResult(bool scriptResult, ScriptScope scope)
        {
            return scriptResult? @"{{#if ""__true__""}}" : @"{{#if __false__}}";
        }
    }
}
