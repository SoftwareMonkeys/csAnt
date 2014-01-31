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
            var binDir = CurrentDirectory
                + Path.DirectorySeparatorChar
                + "bin"
                + Path.DirectorySeparatorChar
                    + GetBuildMode();

            if (!Directory.Exists (binDir))
                Directory.CreateDirectory(binDir);
                    
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
                var name = Path.GetFileNameWithoutExtension (scriptPath);

                if (scriptNames == null
                    || scriptNames.Length == 0
                    || Array.IndexOf (scriptNames, name) > -1) {
                    var assemblyFile = binDir
                        + Path.DirectorySeparatorChar
                        + Path.GetFileNameWithoutExtension (scriptPath)
                        + ".exe";
                
                    Console.WriteLine ("  " + name);
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
                        }
                        else
                            Console.WriteLine ("    Assembly file found. Skipping compile...");
                    } catch (Exception ex) {
                        Error ("Cannot compile '" + name + "' script.", ex);
                    }
                }
            }
                
            if (IsVerbose) {
                Console.WriteLine ("");
            }
        }
    }
}

