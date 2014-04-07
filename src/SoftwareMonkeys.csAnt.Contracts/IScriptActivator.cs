using System;
namespace SoftwareMonkeys.csAnt
{
    public interface IScriptActivator
    {
        IScript ActivateScript(string scriptName);
        IScript ActivateScriptAt(string workingDirectory, string scriptName);
        IScript ActivateScriptFromFile(string scriptFilePath);
    }
}

