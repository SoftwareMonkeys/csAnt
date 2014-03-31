using System;
using SoftwareMonkeys.csAnt.Processes;
using System.IO;
using SoftwareMonkeys.csAnt.Versions;


namespace SoftwareMonkeys.csAnt.External.Nuget
{
    public class NugetPacker
    {
        public NugetPacker ()
        {
        }

        public void PackAll(string projectDirectory)
        {
            Console.WriteLine("");
            Console.WriteLine("Packing all nuget packages in:");
            Console.WriteLine(projectDirectory);
            Console.WriteLine("");

            var pkgsDir = projectDirectory
                + Path.DirectorySeparatorChar
                    + "pkg";

            foreach (var specFile in Directory.GetFiles(pkgsDir, "*.nuspec"))
            {
                PackFile(specFile);
            }
        }
        
        public void Pack(string projectDirectory, string packageName)
        {
            Console.WriteLine("");
            Console.WriteLine("Packing nuget package: " + packageName);
            Console.WriteLine("Project directory:");
            Console.WriteLine(projectDirectory);
            Console.WriteLine("");

            var packageFile = projectDirectory
                + Path.DirectorySeparatorChar
                    + "pkg"
                    + Path.DirectorySeparatorChar
                    + packageName
                    + ".nuspec";

            if (!File.Exists(packageFile))
                throw new ArgumentException("Cannot find '" + packageName + "' package in '" + projectDirectory + "' project directory, at '" + packageFile + "'.");
        
            PackFile(packageFile);
        }

        public void PackFile(string filePath)
        {
            Console.WriteLine("");
            Console.WriteLine("Packing nuget package: " + Path.GetFileNameWithoutExtension(filePath));
            Console.WriteLine("File path:");
            Console.WriteLine(filePath);
            Console.WriteLine("");

            var cmd = "mono";

            var outputDir = Environment.CurrentDirectory
                + Path.DirectorySeparatorChar
                + "pkg"
                + Path.DirectorySeparatorChar
                + Path.GetFileNameWithoutExtension(filePath);

            if (!Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);

            var versionManager = new VersionManager();
            var version = versionManager.GetVersion(Environment.CurrentDirectory);

            var arguments = "lib/nuget.exe"
                + " pack"
                + " " + filePath.Replace(Environment.CurrentDirectory, "").Trim(Path.DirectorySeparatorChar)
                + " -basepath " + Environment.CurrentDirectory
                + " -outputdirectory " + outputDir
                + " -version " + version
                + " -verbosity detailed";

            var starter = new ProcessStarter();
            starter.Start(cmd, arguments);
        }

    }
}

