using System;
using System.IO;
using CSScriptLibrary;
using SoftwareMonkeys.csAnt.IO;
using System.Collections.Generic;


namespace SoftwareMonkeys.csAnt
{
    public class ScriptCompiler : IScriptCompiler
    {
        public bool IsVerbose { get;set; }

        public string BuildMode = "Release";

        public ScriptFileNamer FileNamer { get;set; }

        public ScriptCompiler ()
        {
            FileNamer = new ScriptFileNamer();
        }
        
        public void CompileAll()
        {
            Compile(new string[]{});
        }

        public void Compile(params string[] scriptNames)
        {
            Compile(false, scriptNames);
        }

        public void Compile(bool force, params string[] scriptNames)
        {
            // TODO: Remove binDir variable if not needed. Use ScriptAssembliesDirectory property instead
            var binDir = Environment.CurrentDirectory
                + Path.DirectorySeparatorChar
                + "bin"
                + Path.DirectorySeparatorChar
                    + BuildMode;

            if (!Directory.Exists (binDir))
                Directory.CreateDirectory(binDir);

            Console.WriteLine ("");
            Console.WriteLine ("Compiling scripts...");

            if (IsVerbose) {
                Console.WriteLine ("Bin directory:");
                Console.WriteLine (binDir);
                Console.WriteLine ("");
            }

            var totalCompiled = 0;
            var totalFailed = 0;
            var totalSkipped = 0;

            var scriptFiles = GetScriptFiles ();

            foreach (var scriptPath in scriptFiles) {
                
                var scriptName = Path.GetFileNameWithoutExtension (scriptPath);

                if (scriptNames == null
                    || scriptNames.Length == 0
                    || Array.IndexOf (scriptNames, scriptName) > -1) {
                    var assemblyFile = FileNamer.GetScriptAssemblyPath(scriptName);
                    
                    DirectoryChecker.EnsureDirectoryExists(FileNamer.AssembliesDirectory);
                
                    Console.WriteLine ("  " + scriptName);
                    
                    if (IsVerbose) {
                        Console.WriteLine ("  Script path:");
                        Console.WriteLine ("  " + scriptPath);
                        Console.WriteLine ("  Assembly path:");
                        Console.WriteLine ("  " + assemblyFile);
                    }

                    try {
                        if (!File.Exists(assemblyFile) || force)
                        {
                            Console.WriteLine ("    Compiling...");
                            CSScript.Compile (scriptPath, assemblyFile, BuildMode == "Debug", new string[]{});
                            Console.WriteLine ("    Successful");
                            
                            totalCompiled++;
                        }
                        else
                        {
                            Console.WriteLine ("    Assembly file found. Skipping compile...");
                            totalSkipped++;
                        }
                    } catch (Exception ex) {
                        Console.WriteLine("Script '" + scriptName + "' compile error:");
                        Console.WriteLine(ex.ToString());
                        totalFailed++;
                    }
                }
            }
            
            Console.WriteLine ("");
            Console.WriteLine ("Total compiled: " + totalCompiled);
            Console.WriteLine ("Total skipped: " + totalSkipped);
            Console.WriteLine ("Total failed: " + totalFailed);
            Console.WriteLine ("");
            Console.WriteLine ("Finished!");
            Console.WriteLine ("");
        }

        public string[] GetScriptFiles()
        {
            // TODO: Move to a ScriptsDirectory property
            var scriptsDir = Environment.CurrentDirectory
                + Path.DirectorySeparatorChar
                    + "scripts";
            
            if (IsVerbose) {
                Console.WriteLine ("Scripts directory:");
                Console.WriteLine (scriptsDir);
            }

            var scriptFiles = new List<string> ();
            scriptFiles.AddRange (Directory.GetFiles (scriptsDir, "*.cs"));

            foreach (var appDir in Directory.GetDirectories(Path.GetFullPath("apps"))) {
                var appScriptsDir = appDir + Path.DirectorySeparatorChar + "scripts";
                scriptFiles.AddRange (Directory.GetFiles (appScriptsDir, "*.cs"));
            }

            return scriptFiles.ToArray ();
        }
    }
}
