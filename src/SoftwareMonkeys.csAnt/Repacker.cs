using System;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.Processes;
using System.Collections.Generic;
using System.IO;


namespace SoftwareMonkeys.csAnt
{
    public class Repacker
    {
        public string ILRepackAssemblyPath = "lib/ILRepack.1.25.0/tools/ILRepack.exe";

        public string AssemblyPath { get;set; }

        public string[] Dependencies { get;set; }

        public string PackedBinDirectory = "bin/Release/packed";

        public string Target = "exe";

        public bool IsVerbose = false;

        public DotNetProcessStarter Starter = new DotNetProcessStarter();

        public Repacker (string assemblyPath, string[] dependencies)
        {
            AssemblyPath = assemblyPath;
            Dependencies = dependencies;
        }

        public void Repack()
        {
            Console.WriteLine("");
            Console.WriteLine("Repacking " + Path.GetFileName(AssemblyPath) + " file to include " + Dependencies.Length + " dependencies.");
            Console.WriteLine("");

            Console.WriteLine("Assembly path:");
            Console.WriteLine("  " + AssemblyPath);
            Console.WriteLine("");

            Console.WriteLine("Dependencies:");
            foreach (var dependency in Dependencies)
                Console.WriteLine("  " + dependency);
            Console.WriteLine("");

            var outFile = PackedBinDirectory
                + Path.DirectorySeparatorChar
                    + Path.GetFileName(AssemblyPath);
            
            var arguments = new List<string>();

            var isNewer = File.GetLastWriteTime(outFile) < File.GetLastWriteTime(PathConverter.ToAbsolute(AssemblyPath));
            if (!File.Exists(outFile) || isNewer)
            {
                // Output
                arguments.Add("/out:" + outFile);
    
                // Target type
                arguments.Add("/target:" + Target);
    
                // Verbose
                if (IsVerbose)
                    arguments.Add("/verbose");
    
                // Assembly path
                arguments.Add(PathConverter.ToRelative(AssemblyPath));
    
                // Dependencies
                arguments.AddRange(Dependencies);
                
                Console.WriteLine("Output file:");
                Console.WriteLine("  " + outFile);
                Console.WriteLine("");
    
                DirectoryChecker.EnsureDirectoryExists(PathConverter.ToAbsolute(PackedBinDirectory));
                
                Starter.Start(ILRepackAssemblyPath, arguments.ToArray());
    
                File.SetLastWriteTime(PathConverter.ToAbsolute(outFile), File.GetLastWriteTime(PathConverter.ToAbsolute(AssemblyPath)));
    
                Console.WriteLine("");
                Console.WriteLine("Repacking complete.");
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine("Repacked files are up to date. Skipping repack.");
                Console.WriteLine("");
            }
        }
    }
}

