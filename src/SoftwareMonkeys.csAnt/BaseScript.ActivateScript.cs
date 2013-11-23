using System;
using System.Reflection;
using CSScriptLibrary;
using System.IO;

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

		public IScript ActivateScriptFromFile (string scriptPath)
        {
            if (String.IsNullOrEmpty(scriptPath))
                throw new ArgumentException("scriptPath", "A script path must be provided.");

            if (!File.Exists (scriptPath)) {
                throw new ArgumentException ("Can't find the script file: " + scriptPath);
            }

			var scriptName = Path.GetFileNameWithoutExtension(scriptPath);

			if (IsVerbose) {
				Console.WriteLine("");
				Console.WriteLine("Activating script: " + scriptName);
				Console.WriteLine("Script path:");
				Console.WriteLine("  " + scriptPath);
				Console.WriteLine("");
			}

			CSScript.GlobalSettings.DefaultArguments = "/nl /dbg"; // TODO: This doesn't seem to be working
            CSScript.GlobalSettings.ReportDetailedErrorInfo = IsDebug;

			var assemblyFile = CurrentDirectory
				+ Path.DirectorySeparatorChar
				+ "bin"
				+ Path.DirectorySeparatorChar
				+ GetBuildMode()
				+ Path.DirectorySeparatorChar
				+ Path.GetFileNameWithoutExtension(scriptPath)
				+ ".dll";

			EnsureDirectoryExists(Path.GetDirectoryName(assemblyFile));
			
			if (IsVerbose) {
				Console.WriteLine("");
				Console.WriteLine("Assembly file...");
				Console.WriteLine(assemblyFile);
				Console.WriteLine("");
			}

			// Load the script assembly
			Assembly a = CSScript.Load(scriptPath, assemblyFile, IsDebug, new string[]{});

			// Get the script type
			var type = a.GetTypes () [0];

            var s = (IScript)Activator.CreateInstance(type);

			IScript script = s;

            script.Construct (scriptName, this);

            // Set the indent of the new script to be one more than the current script
            script.Indent = Indent + 1;

			return script;
		}
	}
}

