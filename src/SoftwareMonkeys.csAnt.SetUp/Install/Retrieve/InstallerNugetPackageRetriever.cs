using System;
using SoftwareMonkeys.csAnt.External.Nuget;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using SoftwareMonkeys.csAnt.IO;
using NuGet;

namespace SoftwareMonkeys.csAnt.SetUp.Install.Retrieve
{
    public class InstallerNugetPackageRetriever : BaseInstallerRetriever
    {
        private string nugetSourcePath;
        public string NugetSourcePath
        {
            get { return nugetSourcePath; }
            set
            {
                nugetSourcePath = value;
                if (Versioner != null)
                    Versioner.NugetSourcePath = value;
            }
        }

        public string NugetPath
        {
            get { return NugetChecker.NugetPath; }
            set { NugetChecker.NugetPath = value; }
        }

        public NugetChecker NugetChecker { get;set; }

        public NugetExecutor NugetExecutor { get;set; }

        public string DestinationPath { get;set; }

        public bool IsVerbose { get;set; }

        public NugetVersioner Versioner { get;set; }
        
        public InstallerNugetPackageRetriever (string nugetSourcePath, string destinationPath)
        {
            if (!String.IsNullOrEmpty(destinationPath))
                DestinationPath = destinationPath;
            else
                DestinationPath = Environment.CurrentDirectory;

            if (!String.IsNullOrEmpty(nugetSourcePath))
                NugetSourcePath = nugetSourcePath;

            NugetChecker = new NugetChecker();
            NugetExecutor = new NugetExecutor();
            Versioner = new NugetVersioner(nugetSourcePath);

        }

        public InstallerNugetPackageRetriever (string destinationPath)
        {
            if (!String.IsNullOrEmpty(destinationPath))
                DestinationPath = destinationPath;
            else
                DestinationPath = Environment.CurrentDirectory;

            NugetSourcePath = "https://www.myget.org/F/softwaremonkeys/";
            NugetChecker = new NugetChecker();
            NugetExecutor = new NugetExecutor();
            Versioner = new NugetVersioner(NugetSourcePath);
        }

        public InstallerNugetPackageRetriever ()
            : this("")
        {
        }

        public InstallerNugetPackageRetriever (
            string destinationPath,
            NugetChecker checker,
            NugetExecutor executor
            )
        {
            if (!String.IsNullOrEmpty(destinationPath))
                DestinationPath = destinationPath;
            else
                DestinationPath = Environment.CurrentDirectory;

            NugetSourcePath = "https://www.myget.org/F/softwaremonkeys/";
            NugetChecker = checker;
            NugetExecutor = executor;
            Versioner = new NugetVersioner(NugetSourcePath);
        }
        
        public override void Retrieve (string packageName)
        {
            Retrieve(packageName, new Version(0,0,0,0), String.Empty);
        }

        public override void Retrieve (string packageName, Version version, string status)
        {
            Console.WriteLine("");
            Console.WriteLine("Nuget path:");
            Console.WriteLine("  " + NugetPath);
            Console.WriteLine("");
            Console.WriteLine("Nuget feed path:");
            Console.WriteLine("  " + NugetSourcePath);
            Console.WriteLine("");
            Console.WriteLine("Version: " + version.ToString());
            Console.WriteLine("Status: " + status);
            Console.WriteLine("");

            InstallNuget();

            // TODO: Move this to a config file
            var outputDir = DestinationPath
                + Path.DirectorySeparatorChar
                    + "lib";

            var outputCsAntDir = outputDir
                + Path.DirectorySeparatorChar
                    + "csAnt";

            var arguments = new List<string>();
            arguments.Add("install");
            arguments.Add(packageName);
            arguments.Add(String.Format("-OutputDirectory \"{0}\"", outputDir));
            arguments.Add(String.Format("-Source \"{0}\"", NugetSourcePath));
            arguments.Add("-NoCache"); // TODO: Is this required? It slows down setup and tests, but ensures the latest version of packages are accessible

            // If a status is specified then allow prereleases to access the one with that status
            if (!String.IsNullOrEmpty(status)) 
                arguments.Add("-Pre");

            AddVersionArgument(packageName, version, status, arguments);

            if (!Directory.Exists(outputCsAntDir))
                Directory.CreateDirectory(outputCsAntDir);

            // TODO: Move the executor to a property
            NugetExecutor.Execute(
                arguments.ToArray()
            );
        }

        public void AddVersionArgument(string packageName, Version version, string status, List<string> arguments)
        {
            if (version > new Version(0, 0, 0, 0)
                || !String.IsNullOrEmpty(status))
            {
                var versionString = Versioner.GetVersion(packageName, version, status)
                    + "-" + status;

                Console.WriteLine("Version string: " + versionString);

                arguments.Add("-Version " + versionString.ToString());
            }
        }

        public void InstallNuget()
        {
            NugetChecker.CheckNuget();
        }

    }
}

