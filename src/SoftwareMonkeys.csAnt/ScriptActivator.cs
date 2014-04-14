using System;
using System.IO;
using SoftwareMonkeys.csAnt.IO;
using CSScriptLibrary;
using csscript;
using System.Reflection;


namespace SoftwareMonkeys.csAnt
{
    public class ScriptActivator : IScriptActivator
    {
        public ScriptFileNamer FileNamer { get;set; }

        public bool IsVerbose { get;set; }

        public bool IsDebug { get;set; }

        public IScript ParentScript { get;set; }

        public ScriptActivator ()
        {
            FileNamer = new ScriptFileNamer();
        }

        public ScriptActivator (IScript parentScript)
        {
            ParentScript = parentScript;
            FileNamer = new ScriptFileNamer();
        }

        public T ActivateScript<T> (string scriptName)
            where T : IScript
        {
            var path = FileNamer.GetScriptPath(scriptName);

            return (T)ActivateScriptFromFile(path);
        }

        public IScript ActivateScript (string scriptName)
        {
            var path = FileNamer.GetScriptPath(scriptName);

            if (String.IsNullOrEmpty(path))
                throw new ScriptNotFoundException(scriptName);

            return ActivateScriptFromFile(path);
        }
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

            var assemblyFile = FileNamer.GetScriptAssemblyPath (scriptName);

            DirectoryChecker.EnsureDirectoryExists (Path.GetDirectoryName (assemblyFile));
         
            if (IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Assembly file...");
                Console.WriteLine ("  " + assemblyFile);
                Console.WriteLine ("");
            }

            // TODO: Check if settings are needed
            var scriptSettings = new Settings ();
            // TODO: Check if needed.
            // Could be causing errors with tmp dir access exception when using jenkins.
            // Keeping this line seems to fix errors with activation during install tests (intermittent failure)
            scriptSettings.InMemoryAsssembly = true;
            scriptSettings.HideCompilerWarnings = true;

            // TODO: Remove if not needed. Should be compiled during CSScript.Load
            /*// If the script file is newer then recompile
            if (
                File.Exists (assemblyFile)
                && File.GetLastWriteTime (scriptPath) > File.GetLastWriteTime (assemblyFile)
            ) {
                CSScript.CacheEnabled = false; // TODO: Check if this is needed. Being true was causing errors when running under jenkins.
                CSScript.GlobalSettings = scriptSettings;
                
                // Compile the script
                CSScript.Compile(scriptPath, assemblyFile, IsDebug);

            }*/

            IScript script = null;
            Assembly scriptAssembly = null;

            // Load the script assembly
            try
            {
                CSScript.CacheEnabled = false;
                CSScript.GlobalSettings = scriptSettings;
                scriptAssembly = CSScript.Load(scriptPath, assemblyFile, IsDebug, new string[]{});
                
                // Set the assembly file last write time to the same as the script file, so it's easy to know they're matching
                File.SetLastWriteTime(assemblyFile, File.GetLastWriteTime(scriptPath));
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred when loading the '" + scriptName + "' script.", ex);
            }

            // Get the script type
            var type = scriptAssembly.GetTypes () [0];

            script = (IScript)Activator.CreateInstance(type);
    
            script.Construct (scriptName, ParentScript);
    
            return script;
        }
        
        public IScript ActivateScriptAt(string scriptName, string workingDirectory)
        {
            var scriptPath = FileNamer.GetScriptPath(scriptName, workingDirectory);

            var script = ActivateScriptFromFile(scriptPath);

            script.Relocate(workingDirectory);

            return script;
        }
    }
}

