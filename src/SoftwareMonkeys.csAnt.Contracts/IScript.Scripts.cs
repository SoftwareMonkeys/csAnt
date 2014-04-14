using System;

namespace SoftwareMonkeys.csAnt
{
    public partial interface IScript
    {
        #region Script functions
        IScriptCompiler ScriptCompiler { get;set; }
        IScriptExecutor ScriptExecutor { get;set; }

        void ExecuteScript(string scriptName, params string[] arguments);
        void ExecuteScriptFile(string scriptFileName, params string[] arguments);
        void ExecuteScriptAt(string workingDirectory, string scriptName, params string[] arguments);
        #endregion

    }
}

