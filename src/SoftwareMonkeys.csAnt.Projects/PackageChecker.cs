using System;
using System.IO;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt.Projects
{
    // TODO: Should this be moved to ...External.Nuget library?
    public class PackageChecker
    {
        public string PackageScript = "CyclePackage";

        public string PackagesDir = "pkg";

        public Version CurrentVersion = new Version(0,0,0,0);

        public ScriptExecutor Executor { get;set; }

        public bool IsVerbose { get;set; }

        public PackageChecker (Version currentVersion)
        {
            CurrentVersion = currentVersion;
            Executor = new ScriptExecutor();
        }

        public void Check()
        {
            Console.WriteLine("");
            Console.WriteLine("Checking whether packages (*.nupkg files) are up to date...");
            Console.WriteLine("");

            foreach (var specFile in Directory.GetFiles(PackagesDir, "*.nuspec"))
            {
                var packageName = Path.GetFileNameWithoutExtension(specFile);

                Check(packageName);
            }
        }

        public void Check(string packageName)
        {
            Console.WriteLine("");
            Console.WriteLine("Checking whether '" + packageName + "' package is up to date.");
            Console.WriteLine("");

            if (RequiresPackage(packageName))
            {
                Console.WriteLine("'" + packageName + "' package is NOT up to date.");

                Executor.Execute(PackageScript, packageName, "-skipincrement");
            }
            else
                Console.WriteLine("'" + packageName + "' package is up to date.");

            Console.WriteLine("");
        }

        public bool RequiresPackage(string packageName)
        {
            var version = GetLatestPackageVersion(packageName);

            Console.WriteLine ("Latest package version: " + version);
            Console.WriteLine ("Current version: " + CurrentVersion);

            // Fix the version and remove the last number otherwise it messes up the version checking
            // (the last number isn't necessary as it's not included in package version numbers)
            var currentVersionString = CurrentVersion.ToString ();
            var fixedCurrentVersionString = currentVersionString.Substring (0, currentVersionString.LastIndexOf ("."));

            var fixedVersion = new Version (fixedCurrentVersionString);

            Console.WriteLine ("Fixed version: " + fixedVersion);

            // If the current version is newer than the package version return true
            return (fixedVersion > version);
        }

        public Version GetLatestPackageVersion(string packageName)
        {
            var dir = PathConverter.ToAbsolute("pkg/" + packageName);
            
            var version = new Version (0, 0, 0, 0);

            if (Directory.Exists (dir)) {
                var latestFilePath = FileNavigator.GetNewestFile (dir);

                if (!String.IsNullOrEmpty (latestFilePath))
                    version = GetVersionFromPackageFileName (latestFilePath);
            }

            return version;
        }

        public Version GetVersionFromPackageFileName(string packageFilePath)
        {
            var fileName = Path.GetFileNameWithoutExtension(packageFilePath);

            Console.WriteLine("Package file: " + fileName);

            var releaseName = Path.GetFileName(Path.GetDirectoryName(packageFilePath));

            var prefix = releaseName + ".";

            if (IsVerbose)
                Console.WriteLine("Prefix: " + prefix);

            var withoutPrefix = fileName.Replace(prefix, "");

            if (IsVerbose)
                Console.WriteLine("Without prefix: " + withoutPrefix);

            var versionStartPos = 0;

            var versionLength = withoutPrefix.Length;

            if (withoutPrefix.Contains("-"))
                versionLength = withoutPrefix.IndexOf("-");

            var versionString = withoutPrefix.Substring(versionStartPos, versionLength);

            versionString = versionString.Replace("-", ".");

            return new Version(versionString);
        }
    }
}

