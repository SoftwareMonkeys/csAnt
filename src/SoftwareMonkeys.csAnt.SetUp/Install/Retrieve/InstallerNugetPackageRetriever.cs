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

                Console.WriteLine("Version string: " + versionString);

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

            if (versions.Length > 0)
            {
                var list = new List<string>(versions);
                list.Sort();

                var latestVersion = list[list.Count-1];

                var versionPart = latestVersion.Substring(0, latestVersion.IndexOf("-"));

                return new Version(versionPart);
            }
            else
                return new Version(0,0,0,0);
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

        public bool VersionMatches(string versionWithStatus, Version versionQuery, string status)
        {
            Console.WriteLine("");
            Console.WriteLine("Checking whether version matches...");
            Console.WriteLine("Value (version with status): " + versionWithStatus);
            Console.WriteLine("Version query to match: " + versionQuery);
            Console.WriteLine("Status: " + status);

            var versionStringParts = versionWithStatus.Split('-');
            var versionPart = "";
            var statusPart = "";

            if (versionStringParts.Length == 1)
                versionPart = versionWithStatus;
            else if (versionStringParts.Length == 2)
            {
                versionPart = versionStringParts[0];
                statusPart = versionStringParts[1];
            }

            var statusMatches = status.Equals(statusPart);

            var versionMatches = VersionMatches(versionPart, versionQuery);

            var matches = versionMatches && statusMatches;

            Console.WriteLine("Matches: " + matches.ToString());
            Console.WriteLine("");

            return matches;
        }
        
        public bool VersionMatches(string version, Version versionQuery)
        {
            // If the version query is 0.0.0.0 then it can match any version
            if (versionQuery == new Version(0,0,0,0))
            {
                return true;
            }
            else
            {
                var versionMatches = true;
                
                var versionStringParts = version.Split('.');
                var versionQueryParts = versionQuery.ToString().Split('.');

                for (int i = 0; i < versionStringParts.Length &&  i < versionQueryParts.Length; i++)
                {
                    var sectionQuery = versionQuery.ToString().Split('.')[i];
                    var otherSection = version.Split('.')[i];

                    if (!sectionQuery.Equals(otherSection))
                        versionMatches = false;
                }

                return versionMatches;
            }
        }

        public string[] GetVersions(string packageName)
        {
            var sourceRepository = PackageRepositoryFactory.Default.CreateRepository(NugetSourcePath);

            var packageManager = new PackageManager(sourceRepository, DestinationPath);

            var packages = packageManager.SourceRepository.GetPackages();

            var versions = new List<string>();

            foreach (var package in packages)
            {
                if (package.Id.ToLower() == packageName.ToLower())
                    versions.Add(package.Version.ToString());
            }

            return versions.ToArray();
            /*NugetExecutor.Execute(
                "list",
                packageName,
                "-Source " + NugetSourcePath,
                "-Pre"
            );

            var content = NugetExecutor.Starter.Output;

            return content.Split(new [] { '\r', '\n' });*/

            /*
            /*var program = new Program();
            var console = new NuGet.Common.Console();

            var fs = new PhysicalFileSystem(DestinationPath);
            program.invoke("Initialize", fs,  console);

            var commands = program.Commands.ToDictionary((command)=>command.CommandName);
            var cmd = commands["list"];

            var packages = cmd.GetPackages();*/

            /*var cmd = new ListCommand();
            cmd.Arguments.Add(packageName);
            cmd.Arguments.Add("-source " + NugetSourcePath);
            cmd.Arguments.Add("-nocache");

            var packages = cmd.GetPackages();

            var versions = from p in packages
                select p.Version.ToString();

            return versions.ToArray();*/
        }

    }
}

