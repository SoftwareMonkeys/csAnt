using System;
using SoftwareMonkeys.csAnt.Processes;
using System.IO;
using SoftwareMonkeys.csAnt.Versions;
using System.Collections.Generic;
using SoftwareMonkeys.csAnt.IO;


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

        public string Branch { get; set; }

        public string[] StandardBranches = new string[]{"master", "dev"};

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
        
        public string Pack(string sourceDirectory, string packageName)
        {
            Console.WriteLine("");
            Console.WriteLine("Packing nuget package: " + packageName);
            Console.WriteLine("Source directory:");
            Console.WriteLine(sourceDirectory);
            Console.WriteLine("");

            var packageSpecFile = sourceDirectory
                + Path.DirectorySeparatorChar
                    + "pkg"
                    + Path.DirectorySeparatorChar
                    + packageName
                    + ".nuspec";

            if (!File.Exists(packageSpecFile))
                throw new ArgumentException("Cannot find '" + packageName + "' package in '" + sourceDirectory + "' project directory, at '" + packageSpecFile + "'.");
        
            return PackageFile(packageSpecFile);
        }

        public string PackageFile(string packageSpecFilePath)
        {
            Console.WriteLine("");
            Console.WriteLine("Packing nuget package: " + Path.GetFileNameWithoutExtension(packageSpecFilePath));
            Console.WriteLine("Package spec path:");
            Console.WriteLine(packageSpecFilePath);
            Console.WriteLine("");

            var cmd = "lib"
                + Path.DirectorySeparatorChar
                + "nuget.exe";

            var outputDir = WorkingDirectory
                + Path.DirectorySeparatorChar
                + "pkg"
                + Path.DirectorySeparatorChar
                + Path.GetFileNameWithoutExtension(packageSpecFilePath);

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

                if (!String.IsNullOrEmpty(Branch)
                    && !IsStandardBranch(Branch))
                    versionString = versionString
                        + "-" + Branch;
            }

            var arguments = new List<string>();
            arguments.Add("pack");
            arguments.Add(Starter.FixArgument(packageSpecFilePath.Replace(WorkingDirectory, "").Trim(Path.DirectorySeparatorChar)));
            arguments.Add("-basepath " + Starter.FixArgument(WorkingDirectory));
            arguments.Add("-outputdirectory " + Starter.FixArgument(outputDir));
            if (!String.IsNullOrEmpty(versionString))
                arguments.Add("-version " + versionString);
            arguments.Add("-verbosity detailed");

            Starter.Start(cmd, arguments.ToArray());

            return FileNavigator.GetNewestFile (outputDir); // TODO: Is this the best way to get the package file name? Or should it be created manually?
        }

        public bool IsStandardBranch(string branch)
        {
            return Array.IndexOf (StandardBranches, branch) > -1;
        }
    }
}
