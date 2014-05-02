using System;
using System.Collections.Generic;
using NuGet;

namespace SoftwareMonkeys.csAnt.External.Nuget
{
    public class NugetVersioner
    {
        public bool IsVerbose { get;set; }

        public string NugetSourcePath { get;set; }

        public NugetVersioner ()
        {
        }

        public NugetVersioner (string nugetSourcePath)
        {
            NugetSourcePath = nugetSourcePath;
        }

        public Version GetVersion(string packageName)
        {
            return GetVersion(packageName, "");
        }

        public Version GetVersion(string packageName, string status)
        {
            return GetVersion(packageName, new Version(0,0,0,0), status);
        }

        public Version GetVersion(string packageName, Version version, string status)
        {
            var versions = GetMatchingVersions(packageName, version, status);

            if (versions.Length > 0)
            {
                var list = new List<string>(versions);
                list.Sort();

                var latestVersion = list[list.Count-1];

                var versionPart = latestVersion;
                if (latestVersion.Contains("-"))
                    versionPart = latestVersion.Substring(0, latestVersion.IndexOf("-"));

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
            if (IsVerbose)
            {
                Console.WriteLine("");
                Console.WriteLine("Checking whether version matches...");
                Console.WriteLine("Value (version with status): " + versionWithStatus);
                Console.WriteLine("Version query to match: " + versionQuery);
                Console.WriteLine("Status: " + status);
            }

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

            if (IsVerbose)
            {
                Console.WriteLine("Status matches: " + statusMatches.ToString());
                Console.WriteLine("Version matches: " + statusMatches.ToString());
                Console.WriteLine("Total match: " + matches.ToString());
                Console.WriteLine("");
            }

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

            var packageManager = new PackageManager(sourceRepository, Environment.CurrentDirectory);

            var packages = packageManager.SourceRepository.GetPackages();

            var versions = new List<string>();

            foreach (var package in packages)
            {
                if (package.Id.ToLower() == packageName.ToLower())
                    versions.Add(package.Version.ToString());
            }

            return versions.ToArray();
        }
    }
}

