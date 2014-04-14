using System;
namespace SoftwareMonkeys.csAnt
{
    public interface IScriptExecutor
    {
        IScriptActivator Activator { get;set; }

        IScript Execute(string scriptName, params string[] arguments);
        IScript Execute(IScript script, params string[] arguments);
    }
}

