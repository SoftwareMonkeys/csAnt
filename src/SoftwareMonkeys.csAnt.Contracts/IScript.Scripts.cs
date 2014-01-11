using System;

namespace SoftwareMonkeys.csAnt
{
    public partial interface IScript
    {
        #region Script functions
        T ActivateScript<T>(string scriptName)
            where T : IScript;

        IScript ActivateScript(string scriptName);

        IScript ActivateScriptFromFile(string scriptFilePath);

        void ExecuteScript(string scriptName);
        void ExecuteScriptFile(string scriptFileName);
        #endregion

    }
}

