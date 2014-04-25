using System;
using SoftwareMonkeys.csAnt.External.Nuget;
using System.IO;
using System.Collections.Generic;
using NuGet.Runtime;
using NuGet;
using System.Linq;

namespace SoftwareMonkeys.csAnt.SetUp.Install.Retrieve
{
    public class InstallerNugetPackageRetriever : BaseInstallerRetriever
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
        
        public InstallerNugetPackageRetriever (string nugetSourcePath, string destinationPath)
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

        public InstallerNugetPackageRetriever (string destinationPath)
        {
            if (!String.IsNullOrEmpty(destinationPath))
                DestinationPath = destinationPath;
            else
                DestinationPath = Environment.CurrentDirectory;

            NugetChecker = new NugetChecker();
            NugetExecutor = new NugetExecutor();
            NugetSourcePath = "https://www.myget.org/F/softwaremonkeys/";
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

            InstallNuget();

            // TODO: Move this to a config file
            var outputDir = DestinationPath
                + Path.DirectorySeparatorChar
                    + "lib";

            var outputCsAntDir = outputDir
                + Path.DirectorySeparatorChar
                    + "csAnt";

            if (version == new Version(0,0,0,0))
                version = GetVersion(packageName, version, status);

            var arguments = new List<string>();
            arguments.Add("install");
            arguments.Add(packageName);
            arguments.Add(String.Format("-OutputDirectory \"{0}\"", outputDir));
            arguments.Add(String.Format("-Source \"{0}\"", NugetSourcePath));
            arguments.Add("-NoCache"); // TODO: Is this required? It slows down setup and tests, but ensures the latest version of packages are accessible
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
                var versionString = GetVersion(packageName, version, status)
                    + "-" + status;

                arguments.Add("-Version " + versionString.ToString());
            }
        }

        public void InstallNuget()
        {
            NugetChecker.CheckNuget();
        }

        public Version GetVersion(string packageName, Version version, string status)
        {
            var versions = GetMatchingVersions(packageName, version, status);

            var list = new List<string>(versions);
            list.Sort();

            var latestVersion = list[list.Count-1];

            var versionPart = latestVersion.Substring(0, latestVersion.IndexOf("-"));

            return new Version(versionPart);
        }

        public string[] GetMatchingVersions(string packageName, Version versionQuery, string status)
        {
            var versions = GetVersions(packageName);

            var matchingVersions = new List<string>();

            foreach (var version in versions)
            {
                if (VersionMatches(version, versionQuery, status))
                {
                    matchingVersions.Add(version);
                }
            }

            return matchingVersions.ToArray();
        }

        public bool VersionMatches(string versionStringWithStatus, Version versionQuery, string status)
        {
            var versionStringParts = versionStringWithStatus.Split('-');
            var versionPart = versionStringParts[0];
            var statusPart = versionStringParts[1];

            var versionSections = versionPart.Split('.');

            var statusMatches = status.Equals(statusPart);

            var versionMatches = VersionMatches(versionPart, versionQuery);

            return versionMatches && statusMatches;
        }
        
        public bool VersionMatches(string versionString, Version versionQuery)
        {
            var versionMatches = true;

            for (int i = 0; i < versionQuery.ToString().Split('.').Length; i++)
            {
                var sectionQuery = versionQuery.ToString().Split('.')[i];
                var otherSection = versionString.Split('.')[i];

                if (!sectionQuery.Equals(otherSection))
                    versionMatches = false;
            }

            return versionMatches;
        }

        public string[] GetVersions(string packageName)
        {
            //Connect to the official package repository
            IPackageRepository repo = PackageRepositoryFactory.Default.CreateRepository(NugetSourcePath);

            //Get the list of all NuGet packages with ID 'EntityFramework'       
            List<IPackage> packages = repo.FindPackagesById(packageName).ToList();

            var versions = from p in packages
                select p.Version.ToString();

            return versions.ToArray();
        }
    }
}

