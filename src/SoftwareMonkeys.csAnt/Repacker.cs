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

        public string PackedBinDirectory = "bin/{BuildMode}/packed";

        public string Target = "exe";

        public bool IsVerbose = false;

        public string BuildMode = "Release";

        public DotNetProcessStarter Starter = new DotNetProcessStarter();

        public Repacker (string assemblyPath, string[] dependencies)
        {
            AssemblyPath = assemblyPath;
            Dependencies = dependencies;
        }

        public Repacker (string assemblyPath, string[] dependencies, string buildMode)
        {
            AssemblyPath = assemblyPath;
            Dependencies = dependencies;
            BuildMode = buildMode;
        }

        public void Repack()
        {
            Console.WriteLine("");
            Console.WriteLine("Repacking " + Path.GetFileName(AssemblyPath) + " file to include " + Dependencies.Length + " dependencies.");
            Console.WriteLine("");

            AssemblyPath = FixPath(AssemblyPath, BuildMode);
            Dependencies = FixPaths(Dependencies, BuildMode);

            Console.WriteLine("Current directory:");
            Console.WriteLine("  " + Environment.CurrentDirectory);
            Console.WriteLine("");

            Console.WriteLine("Assembly path:");
            Console.WriteLine("  " + AssemblyPath);
            Console.WriteLine("");

            Console.WriteLine("Build mode:");
            Console.WriteLine("  " + BuildMode);
            Console.WriteLine("");

            Console.WriteLine("Dependencies:");
            foreach (var dependency in Dependencies)
                Console.WriteLine("  " + dependency);
            Console.WriteLine("");

            var outFile = PackedBinDirectory
                + Path.DirectorySeparatorChar
                    + Path.GetFileName(AssemblyPath);

            outFile = FixPath(outFile, BuildMode);
            
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
    
                DirectoryChecker.EnsureDirectoryExists(Path.GetDirectoryName(PathConverter.ToAbsolute(outFile)));
                
                Starter.Start(ILRepackAssemblyPath, arguments.ToArray());
    
                var lastWriteTime = File.GetLastWriteTime(PathConverter.ToAbsolute(AssemblyPath));

                File.SetLastWriteTime(PathConverter.ToAbsolute(outFile), lastWriteTime);
    
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

        public string FixPath(string path, string buildMode)
        {
            return path.Replace("{BuildMode}", buildMode);
        }

        public string[] FixPaths(string[] paths, string buildMode)
        {
            var list = new List<string>(paths);

            for (int i = 0; i < list.Count; i++)
            {
                list[i] = FixPath(list[i], buildMode);
            }

            return list.ToArray();
        }
    }
}

