using System;
using System.Collections.Generic;
using NuGet;

namespace SoftwareMonkeys.csAnt.Versions
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

        public SemanticVersion GetSemanticVersion(string packageName)
        {
            return GetSemanticVersion (packageName, "");
        }

        public Version GetVersion(string packageName, string status)
        {
            return GetVersion(packageName, new Version(0,0,0,0), status);
        }

        public SemanticVersion GetSemanticVersion(string packageName, string status)
        {
            return GetSemanticVersion (packageName, new Version (0, 0, 0, 0), status);
        }
        
        public Version GetVersion(string packageName, Version version, string status)
        {
            return GetSemanticVersion (packageName, version, status).Version;
        }

        public SemanticVersion GetSemanticVersion(string packageName, Version version, string status)
        {
            Console.WriteLine("");
            Console.WriteLine("Getting package version...");
            Console.WriteLine("");
            Console.WriteLine("Package name: " + packageName);
            Console.WriteLine("Version (to match): " + (version != null && version != new Version(0,0,0,0) ? version.ToString() : "[Latest]"));
            Console.WriteLine("Status (to match): " + (!String.IsNullOrEmpty(status) ? status : "[Stable]"));
            Console.WriteLine("");

            var versions = GetMatchingVersions(packageName, version, status);

            if (versions.Length > 0) {
                var list = new List<SemanticVersion> (versions);
                list.Sort ();

                if (IsVerbose) {
                    Console.WriteLine ("Versions found:");

                    foreach (var v in list) {
                        Console.WriteLine ("  " + v);
                    }
                }

                var latestVersion = list [list.Count - 1];

                Console.WriteLine ("");
                Console.WriteLine ("Latest package version: " + latestVersion);

                return latestVersion;
            } else {
                Console.WriteLine ("No matching versions found.");

                return new SemanticVersion (0, 0, 0, 0);
            }
        }

        public SemanticVersion[] GetMatchingVersions(string packageName, Version versionQuery, string status)
        {
            var versions = GetVersions(packageName);

            var matchingVersions = new List<SemanticVersion>();

            foreach (var version in versions)
            {
                if (VersionMatches(version, versionQuery, status))
                {
                    matchingVersions.Add(version);
                }
            }

            return matchingVersions.ToArray();
        }

        public bool VersionMatches(SemanticVersion semanticVersion, Version versionQuery, string status)
        {
            if (IsVerbose)
            {
                Console.WriteLine("");
                Console.WriteLine("Checking whether version matches...");
                Console.WriteLine("Value (version with status): " + semanticVersion);
                Console.WriteLine("Version query to match: " + versionQuery);
                Console.WriteLine("Status: " + status);
            }

            var versionPart = semanticVersion.Version;
            var statusPart = semanticVersion.SpecialVersion;

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

        public bool VersionMatches(Version version, Version versionQuery)
        {
            // If the version query is 0.0.0.0 then it can match any version
            if (versionQuery == new Version(0,0,0,0))
            {
                return true;
            }
            else
            {
                var versionMatches = true;

                var versionStringParts = version.ToString().Split('.');
                var versionQueryParts = versionQuery.ToString().Split('.');

                for (int i = 0; i < versionStringParts.Length &&  i < versionQueryParts.Length; i++)
                {
                    var left = versionQuery.ToString().Split('.')[i];
                    var right = version.ToString().Split('.')[i];

                    if (!left.Equals(right))
                        versionMatches = false;
                }

                return versionMatches;
            }
        }

        public SemanticVersion[] GetVersions(string packageName)
        {
            Console.WriteLine ("Nuget source path: " + NugetSourcePath);

            var sourceRepository = PackageRepositoryFactory.Default.CreateRepository(NugetSourcePath);

            var packageManager = new PackageManager(sourceRepository, Environment.CurrentDirectory);

            var packages = packageManager.SourceRepository.GetPackages();

            var versions = new List<SemanticVersion>();

            foreach (var package in packages)
            {
                if (package.Id.ToLower() == packageName.ToLower())
                    versions.Add(package.Version);
            }

            return versions.ToArray();
        }
    }
}

