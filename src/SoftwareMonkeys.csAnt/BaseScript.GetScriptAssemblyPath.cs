using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        public string GetScriptAssemblyPath(string scriptName)
        {
            return ScriptAssembliesDirectory
                + Path.DirectorySeparatorChar
                + scriptName
                + "Script.exe";
        }
    }
}

