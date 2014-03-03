using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        public string ScriptAssembliesDirectory
        {
            get
            {
                var scriptAssembliesDirectory = String.Empty;
                
                if (String.IsNullOrEmpty(scriptAssembliesDirectory))
                    scriptAssembliesDirectory = CurrentDirectory
                        + Path.DirectorySeparatorChar
                        + "bin"
                        + Path.DirectorySeparatorChar
                        + BuildMode
                        + Path.DirectorySeparatorChar
                        + "scripts";
                
                return scriptAssembliesDirectory;
            }
        }
    }
}

