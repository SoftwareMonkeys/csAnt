using System;
using System.Reflection;
using System.IO;
using csscript;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
        public T ActivateScript<T> (string scriptName)
            where T : IScript
        {
            var path = GetScriptPath(scriptName);

            return (T)ActivateScriptFromFile(path);
        }

        public IScript ActivateScript (string scriptName)
        {
            var path = GetScriptPath(scriptName);

            if (String.IsNullOrEmpty(path))
                throw new ScriptNotFoundException(scriptName);

            return ActivateScriptFromFile(path);
        }
	}
}

