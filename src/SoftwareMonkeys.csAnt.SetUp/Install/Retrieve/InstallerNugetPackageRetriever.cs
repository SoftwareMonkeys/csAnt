using System;
using SoftwareMonkeys.csAnt.External.Nuget;
using System.IO;
using System.Collections.Generic;


namespace SoftwareMonkeys.csAnt.SetUp.Install.Retrieve
{
    public class InstallerNugetRetriever : BaseInstallerRetriever
    {
        public string NugetSourcePath { get;set; }

        public string NugetPath
        {
            get { return NugetChecker.NugetPath; }
            set { NugetChecker.NugetPath = value; }
        }

        public NugetChecker NugetChecker { get;set; }

        public NugetExecutor NugetExecutor { get;set; }

        public string DestinationPath { get;set; }

        public Version Version = new Version(0,0,0,0);

        public string PackageName = "csAnt";
        
        public InstallerNugetRetriever (string nugetSourcePath, string destinationPath, Version version)
        {
            if (!String.IsNullOrEmpty(destinationPath))
                DestinationPath = destinationPath;
            else
                DestinationPath = Environment.CurrentDirectory;

            NugetChecker = new NugetChecker();
            NugetExecutor = new NugetExecutor();

            if (!String.IsNullOrEmpty(nugetSourcePath))
                NugetSourcePath = nugetSourcePath;

            Version = version;
        }

        public InstallerNugetRetriever (string nugetSourcePath, string destinationPath, Version version, NugetChecker checker, NugetExecutor executor)
        {
            if (!String.IsNullOrEmpty(destinationPath))
                DestinationPath = destinationPath;
            else
                DestinationPath = Environment.CurrentDirectory;

            if (!String.IsNullOrEmpty(nugetSourcePath))
                NugetSourcePath = nugetSourcePath;

            NugetChecker = checker;
            NugetExecutor = executor;
            Version = version;
        }

        public InstallerNugetRetriever (string nugetSourcePath, string destinationPath)
        {
            if (!String.IsNullOrEmpty(destinationPath))
                DestinationPath = destinationPath;
            else
                DestinationPath = Environment.CurrentDirectory;

            NugetChecker = new NugetChecker();
            NugetExecutor = new NugetExecutor();

            if (!String.IsNullOrEmpty(nugetSourcePath))
                NugetSourcePath = nugetSourcePath;
        }

        /*public InstallerNugetRetriever (string nugetSourcePath, string nugetPath, string destinationPath)
        {
            if (!String.IsNullOrEmpty(destinationPath))
                DestinationPath = destinationPath;
            else
                DestinationPath = Environment.CurrentDirectory;

            NugetChecker = new NugetChecker();
            NugetExecutor = new NugetExecutor();

            if (!String.IsNullOrEmpty(nugetSourcePath))
                NugetSourcePath = nugetSourcePath;

            if (!String.IsNullOrEmpty(nugetPath))
                NugetPath = nugetPath;
        }*/

        public InstallerNugetRetriever (string destinationPath)
        {
            if (!String.IsNullOrEmpty(destinationPath))
                DestinationPath = destinationPath;
            else
                DestinationPath = Environment.CurrentDirectory;

            NugetChecker = new NugetChecker();
            NugetExecutor = new NugetExecutor();
            NugetSourcePath = "https://www.myget.org/F/softwaremonkeys/";
        }

        public InstallerNugetRetriever ()
            : this("")
        {
        }

        public InstallerNugetRetriever (
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
        }

        public override void Retrieve ()
        {
            Console.WriteLine("");
            Console.WriteLine("Nuget path:");
            Console.WriteLine(NugetPath);
            Console.WriteLine("");
            Console.WriteLine("Nuget feed path:");
            Console.WriteLine(NugetSourcePath);
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
            arguments.Add(PackageName);
            arguments.Add("-OutputDirectory " + outputDir);
            arguments.Add("-Source " + NugetSourcePath);
            arguments.Add("-NoCache"); // TODO: Is this required? It slows down setup and tests, but ensures the latest version of packages are accessible

            if (Version > new Version(0, 0, 0, 0))
                arguments.Add("-Version " + Version.ToString());

            if (!Directory.Exists(outputCsAntDir))
                Directory.CreateDirectory(outputCsAntDir);

            // TODO: Move the executor to a property
            NugetExecutor.Execute(
                arguments.ToArray()
            );
        }

        public void InstallNuget()
        {
            NugetChecker.CheckNuget();
        }

    }
}

