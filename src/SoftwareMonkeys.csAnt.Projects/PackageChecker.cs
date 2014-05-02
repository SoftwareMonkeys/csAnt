using System;
using System.IO;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt.Projects
{
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

                Executor.Execute(PackageScript, packageName);
            }
            else
                Console.WriteLine("'" + packageName + "' package is up to date.");

            Console.WriteLine("");
        }

        public bool RequiresPackage(string packageName)
        {
            var version = GetLatestPackageVersion(packageName);

            // If the current version is newer than the package version return true
            return (CurrentVersion > version);
        }

        public Version GetLatestPackageVersion(string packageName)
        {
            var dir = PathConverter.ToAbsolute("pkg/" + packageName);

            var latestFilePath = FileNavigator.GetNewestFile(dir);

            var version = new Version(0,0,0,0);

            if (!String.IsNullOrEmpty(latestFilePath))
                version = GetVersionFromPackageFileName(latestFilePath);

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
                versionLength = withoutPrefix.LastIndexOf("-");

            var version = withoutPrefix.Substring(versionStartPos, versionLength);

            version = version.Replace("-", ".");

            return new Version(version);
        }
    }
}

