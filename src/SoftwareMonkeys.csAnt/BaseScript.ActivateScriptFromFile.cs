using System;
using System.IO;
using CSScriptLibrary;
using System.Reflection;
using csscript;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        public IScript ActivateScriptFromFile (string scriptPath)
        {
            if (String.IsNullOrEmpty (scriptPath))
                throw new ArgumentException ("scriptPath", "A script path must be provided.");

            if (!File.Exists (scriptPath)) {
                throw new ArgumentException ("Can't find the script file: " + scriptPath);
            }

            var scriptName = Path.GetFileNameWithoutExtension (scriptPath);

            if (IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Activating script: " + scriptName);
                Console.WriteLine ("Script path:");
                Console.WriteLine ("  " + scriptPath);
                Console.WriteLine ("");
            }

            CSScript.GlobalSettings.DefaultArguments = "/nl"; // TODO: Check if this is working
            //CSScript.GlobalSettings.ReportDetailedErrorInfo = IsDebug; // TODO: Check if needed

            var assemblyFile = GetScriptAssemblyPath (scriptName);

            EnsureDirectoryExists (Path.GetDirectoryName (assemblyFile));
         
            if (IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Assembly file...");
                Console.WriteLine ("  " + assemblyFile);
                Console.WriteLine ("");
            }

            // TODO: Check if settings are needed
            var scriptSettings = new Settings ();
            //scriptSettings.InMemoryAsssembly = true; // TODO: Check if needed

            var compilerOptions = "";

            // If the script file is newer then recompile
            if (
                File.Exists (assemblyFile)
                && File.GetLastWriteTime (scriptPath) > File.GetLastWriteTime (assemblyFile)
            ) {
                CSScript.GlobalSettings = scriptSettings;
                
                // Compile the script
                CSScript.Compile(scriptPath, assemblyFile, IsDebug);

                // Set the assembly file last write time to the same as the script file, so it's easy to know they're matching
                File.SetLastWriteTime(assemblyFile, File.GetLastWriteTime(scriptPath));
            }

            // Load the script assembly
            Assembly a = CSScript.LoadWithConfig(scriptPath, assemblyFile, IsDebug, scriptSettings, compilerOptions);

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

