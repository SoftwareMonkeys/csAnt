using System;
using NuGet;

namespace SoftwareMonkeys.csAnt.Versions
{
    /// <summary>
    /// Identifies the current version by checking NuGet and the git origin repository
    /// </summary>
    public class VersionIdentifier
    {
        public GitVersioner GitVersioner { get; set; }
        public NugetVersioner NugetVersioner { get; set; } 
        public string PackageName { get;set; }
        public string Status { get;set; }
        public string Branch { get;set; }

        public VersionIdentifier (string branch, string status, string nugetSourcePath, string packageName)
        {
            GitVersioner = new GitVersioner (branch);
            Status = status;
            NugetVersioner = new NugetVersioner (nugetSourcePath);
            PackageName = packageName;
        }

        public SemanticVersion GetVersion()
        {
            var nugetVersion = GetNuGetVersionVersion ();
            var gitVersion = GetOriginVersion ();
            
            
            Console.WriteLine ("NuGet feed version: " + nugetVersion.ToString ());
            Console.WriteLine ("git origin version: " + gitVersion.ToString ());
            Console.WriteLine ();

            var version = (nugetVersion > gitVersion
                    ? nugetVersion
                    : gitVersion);
            
            Console.WriteLine ("Chosen version: " + version.ToString ());

            return version;
        }

        public SemanticVersion GetNuGetVersionVersion()
        {
            var version = NugetVersioner.GetSemanticVersion(PackageName, Status);


            return version;
        }

        public SemanticVersion GetOriginVersion()
        {
            var version =  GitVersioner.GetOriginVersion ();

            return version;
        }
    }
}

