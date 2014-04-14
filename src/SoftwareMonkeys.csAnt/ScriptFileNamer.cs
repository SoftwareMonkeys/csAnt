using System;
using System.IO;


namespace SoftwareMonkeys.csAnt
{
    public class ScriptFileNamer
    {
        public bool IsVerbose { get;set; }

        public string BuildMode = "Release";
        
        public string AssembliesDirectory
        {
            get
            {
                var assembliesDirectory = String.Empty;
                
                if (String.IsNullOrEmpty(assembliesDirectory))
                    assembliesDirectory = Environment.CurrentDirectory
                        + Path.DirectorySeparatorChar
                        + "bin"
                        + Path.DirectorySeparatorChar
                        + BuildMode
                        + Path.DirectorySeparatorChar
                        + "scripts";
                
                return assembliesDirectory;
            }
        }

        public string GetScriptPath (string scriptName)
        {
            return GetScriptPath(scriptName, Environment.CurrentDirectory);
        }

        public string GetScriptPath(string scriptName, string workingDirectory)
        {
            string scriptPath = string.Empty;
            
            string scriptsDir = GetScriptsPath();
                        
            scriptPath = GetStandardScriptPath(scriptsDir, scriptName);
            
            if (String.IsNullOrEmpty(scriptPath))
            {
                if (IsVerbose)
                    Console.WriteLine ("Looking for compiled script.");

                scriptPath = GetCompiledScriptPath(scriptsDir, scriptName);
            }

            if (String.IsNullOrEmpty(scriptPath))
            {
                throw new ScriptNotFoundException(scriptName);
            }
                        
            return scriptPath;
        }

        public string GetStandardScriptPath(string scriptsDir, string scriptName)
        {
            string scriptPath = String.Empty;

            foreach (var p in Directory.GetFiles(scriptsDir))
            {
                string fileName = Path.GetFileNameWithoutExtension(p);

                string ext = Path.GetExtension(p).Trim('.');
                
                if (fileName.ToLower() == scriptName.Trim().ToLower()
                    && ext == "cs")
                {
                    scriptPath = p;
                }
                
            }

            return scriptPath;
        }

        public string GetCompiledScriptPath(string scriptsDir, string scriptName)
        {
            string scriptPath = String.Empty;

            foreach (var d in Directory.GetDirectories(scriptsDir))
            {
                var s = Path.GetFileName(d);

                if (s == scriptName)
                {
                    string fileName = d
                        + Path.DirectorySeparatorChar
                        + s + ".cs";

                    string fileName2 = d
                        + Path.DirectorySeparatorChar
                        + s + "Script.cs";

                    if (File.Exists(fileName))
                        scriptPath = fileName;

                    if (File.Exists(fileName2))
                        scriptPath = fileName2;
                }
            }

            return scriptPath;
        }

        public string GetScriptsPath()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "scripts");
            
            return path;
            
        }

        public string GetScriptAssemblyPath(string scriptName)
        {
            return AssembliesDirectory
                + Path.DirectorySeparatorChar
                + scriptName
                + "Script.exe";
        }
    }
}

