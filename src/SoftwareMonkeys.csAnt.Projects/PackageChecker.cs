using System;
using System.IO;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.IO.Compression;
using SoftwareMonkeys.FileNodes;

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
            var packageVersion = GetLatestPackageVersion(packageName);

            Console.WriteLine ("Latest nuget package version: " + packageVersion);
            Console.WriteLine ("Current version (in .node file): " + CurrentVersion);

            // If the current version is newer than the package version return true
            return (CurrentVersion > packageVersion);
        }

        public Version GetLatestPackageVersion(string packageName)
        {
            Console.WriteLine ("Getting latest package version...");
            Console.WriteLine ("Package name: " + packageName);

            var dir = PathConverter.ToAbsolute("pkg/" + packageName);
            
            var version = new Version (0, 0, 0, 0);

            if (Directory.Exists (dir)) {
                var latestFilePath = FileNavigator.GetNewestFile (dir);

                if (!String.IsNullOrEmpty (latestFilePath))
                    version = GetVersionFromPackageFile (latestFilePath);
                else
                    Console.WriteLine ("No package file found: " + latestFilePath);
            } else
                Console.WriteLine ("Package directory not found: " + dir);

            return version;
        }

        public Version GetVersionFromPackageFile(string packageFilePath)
        {
            Console.WriteLine ("Getting version from package file...");
            Console.WriteLine ("  " + packageFilePath);

            var tmpDir = Path.GetFullPath(Path.Combine(packageFilePath, "../_tmp"));

            var zipper = new FileZipper ();
            zipper.Unzip (packageFilePath, tmpDir);

            var nodeManager = new FileNodeManager (tmpDir);

            if (nodeManager.CurrentNode == null)
                throw new Exception ("Can't find .node file in: " + tmpDir);

            var version = new Version(nodeManager.CurrentNode.Properties ["Version"]);
            
            Console.WriteLine ("Version: " + version);
            Console.WriteLine ("");

            Directory.Delete (tmpDir, true);

            return version;
        }
    }
}

