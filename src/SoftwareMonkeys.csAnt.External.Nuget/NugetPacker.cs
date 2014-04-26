using System;
using SoftwareMonkeys.csAnt.Processes;
using System.IO;
using SoftwareMonkeys.csAnt.Versions;
using System.Collections.Generic;


namespace SoftwareMonkeys.csAnt.External.Nuget
{
    public class NugetPacker
    {
        public string WorkingDirectory { get;set; }

        public VersionManager VersionManager = new VersionManager();

        public bool AutoLoadVersion = true;

        private Version version = new Version(0,0,0,0);
        public Version Version
        {
            get
            {
                return version;
            }
            set
            {
                version = value;
            }
        }

        private string status;
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }

        public DotNetProcessStarter Starter { get;set; }

        public NugetPacker ()
        {
            Starter = new DotNetProcessStarter();
            WorkingDirectory = Environment.CurrentDirectory;
        }

        public NugetPacker(string workingDirectory)
        {
            Starter = new DotNetProcessStarter();
            WorkingDirectory = workingDirectory;
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
                PackageFile(specFile);
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
        
            PackageFile(packageFile);
        }

        public void PackageFile(string filePath)
        {
            Console.WriteLine("");
            Console.WriteLine("Packing nuget package: " + Path.GetFileNameWithoutExtension(filePath));
            Console.WriteLine("File path:");
            Console.WriteLine(filePath);
            Console.WriteLine("");

            var cmd = "lib"
                + Path.DirectorySeparatorChar
                + "nuget.exe";

            var outputDir = WorkingDirectory
                + Path.DirectorySeparatorChar
                + "pkg"
                + Path.DirectorySeparatorChar
                + Path.GetFileNameWithoutExtension(filePath);

            if (!Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);

            var versionString = "";

            if (Version != null
                && (Version != new Version(0,0,0,0)
                || !String.IsNullOrEmpty(Status)))
            {
                versionString = Version.Major
                    + "."
                    + Version.Minor
                    + "."
                    + Version.Build;

                if (!String.IsNullOrEmpty(Status))
                    versionString = versionString
                        + "-" + Status;
            }

            var arguments = new List<string>();
            arguments.Add("pack");
            arguments.Add(Starter.FixArgument(filePath.Replace(WorkingDirectory, "").Trim(Path.DirectorySeparatorChar)));
            arguments.Add("-basepath " + Starter.FixArgument(WorkingDirectory));
            arguments.Add("-outputdirectory " + Starter.FixArgument(outputDir));
            if (!String.IsNullOrEmpty(versionString))
                arguments.Add("-version " + versionString);
            arguments.Add("-verbosity detailed");

            Starter.Start(cmd, arguments.ToArray());
        }
    }
}
