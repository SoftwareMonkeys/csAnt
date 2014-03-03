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
            // TODO: Remove binDir variable if not needed. Use ScriptAssembliesDirectory property instead
            var binDir = CurrentDirectory
                + Path.DirectorySeparatorChar
                + "bin"
                + Path.DirectorySeparatorChar
                    + GetBuildMode();

            if (!Directory.Exists (binDir))
                Directory.CreateDirectory(binDir);
                    
            // TODO: Move to a ScriptsDirectory property
            var scriptsDir = CurrentDirectory
                + Path.DirectorySeparatorChar
                + "scripts";

            Console.WriteLine ("");
            Console.WriteLine ("Compiling scripts...");

            if (IsVerbose) {
                Console.WriteLine ("Scripts directory:");
                Console.WriteLine (scriptsDir);
                Console.WriteLine ("Bin directory:");
                Console.WriteLine (binDir);
                Console.WriteLine ("");
            }

            foreach (var scriptPath in Directory.GetFiles(scriptsDir, "*.cs")) {
                //bool didCompile = false; // TODO: Finish
                
                var scriptName = Path.GetFileNameWithoutExtension (scriptPath);

                if (scriptNames == null
                    || scriptNames.Length == 0
                    || Array.IndexOf (scriptNames, scriptName) > -1) {
                    var assemblyFile = GetScriptAssemblyPath(scriptName);
                    
                    EnsureDirectoryExists(ScriptAssembliesDirectory);
                
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
                            CSScript.Compile (scriptPath, assemblyFile, IsDebug, new string[]{});
                            Console.WriteLine ("    Successful");
                            
                            //didCompile = true;
                        }
                        else
                            Console.WriteLine ("    Assembly file found. Skipping compile...");
                    } catch (Exception ex) {
                        Error ("Cannot compile '" + scriptName + "' script.", ex);
                        //didCompile = false;
                    }
                }
                
                // TODO: Finish
                //var scriptInfo = new ScriptInfo(scriptName);
                //script.LatestCompiled = didCompile;
            }
                
            if (IsVerbose) {
                Console.WriteLine ("");
            }
        }
    }
}

