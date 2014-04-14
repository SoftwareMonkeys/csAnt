using System;
using System.IO;
using CSScriptLibrary;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        public void CompileScripts ()
        {
            CompileScripts(false);
        }
        
        public void CompileScripts (bool force)
        {
            CompileScripts(force, new string[]{});
        }
        
        public void CompileScripts (params string[] scriptNames)
        {
            CompileScripts(false, scriptNames);
        }
        
        public void CompileScripts (string[] scriptNames, bool force)
        {
            CompileScripts (force, scriptNames);
        }

        public void CompileScripts (bool force, params string[] scriptNames)
        {
            ScriptCompiler.Compile(force, scriptNames);
        }
    }
}

