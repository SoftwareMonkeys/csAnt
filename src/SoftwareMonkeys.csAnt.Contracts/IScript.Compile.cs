using System;

namespace SoftwareMonkeys.csAnt
{
    public partial interface IScript
    {
        #region Compile
        void CompileScripts();

        string GetScriptAssemblyPath(string scriptName);
        #endregion
    }
}

