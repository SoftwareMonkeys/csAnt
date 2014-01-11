using System;
using System.IO;
using System.Linq;
using SoftwareMonkeys.csAnt.IO.Compression;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt.Packages
{
    public class PackageInstaller
    {
        public IFileZipper Zipper { get;set; }

        public PackageInstaller (
            IFileZipper zipper
            )
        {
            Zipper = zipper;
        }
        
        public PackageInstaller ()
        {
            Zipper = new FileZipper();
        }
        
        public void Install (
            string workingDirectory,
            string packagesDirectory,
            string packageName,
            string groupName,
            string externalRepositoryPath
            )
        {
            Console.WriteLine ("");
            Console.WriteLine ("Installing package: " + packageName);
            Console.WriteLine ("Group: " + groupName);

            var file = GetPackagePath(packageName, groupName, externalRepositoryPath);
            
            Console.WriteLine ("");
            Console.WriteLine ("Working directory:");
            Console.WriteLine (workingDirectory);
            Console.WriteLine ("Packages directory:");
            Console.WriteLine (packagesDirectory);
            Console.WriteLine ("External repository:");
            Console.WriteLine (externalRepositoryPath);

            var toFile = file.Replace(
                externalRepositoryPath,
                packagesDirectory
            );

            if (!Directory.Exists(Path.GetDirectoryName(toFile)))
                Directory.CreateDirectory(Path.GetDirectoryName(toFile));

            InstallFile(file, toFile);
        }

        public void Install (
            string workingDirectory,
            string packagesDirectory,
            string packageName,
            string externalRepositoryPath
            )
        {
            Console.WriteLine ("");
            Console.WriteLine ("Installing package: " + packageName);

            var file = GetPackagePath(packageName, externalRepositoryPath);
            
            Console.WriteLine ("");
            Console.WriteLine ("Working directory:");
            Console.WriteLine (workingDirectory);
            Console.WriteLine ("Packages directory:");
            Console.WriteLine (packagesDirectory);
            Console.WriteLine ("External repository:");
            Console.WriteLine (externalRepositoryPath);

            var toFile = file.Replace(
                externalRepositoryPath,
                packagesDirectory
            );
            
            if (!Directory.Exists(Path.GetDirectoryName(toFile)))
                Directory.CreateDirectory(Path.GetDirectoryName(toFile));

            InstallFile(file, toFile);
        }

        public void InstallFile (string packageZipFile, string toFile)
        {
            Console.WriteLine ("");
            Console.WriteLine ("Installing package file:");
            Console.WriteLine(packageZipFile);

            CopyTo(packageZipFile, toFile);

            var dir = Extract(toFile);

            RunInstallScript(dir);
        }

        public void CopyTo (string packageZipFile, string toFile)
        {
            Console.WriteLine ("");
            Console.WriteLine ("Copying package:");
            Console.WriteLine(packageZipFile);

            File.Copy(packageZipFile, toFile, true);

            Console.WriteLine ("To:");
            Console.WriteLine (toFile);
        }

        public string Extract(string zipFile)
        {
            Console.WriteLine ("");
            Console.WriteLine ("Extracting package zip file:");
            Console.WriteLine (zipFile);

            var dir = Path.GetDirectoryName(zipFile);
            
            Console.WriteLine ("To:");
            Console.WriteLine (dir);

            Zipper.Unzip(zipFile, dir, "");

            return dir;
        }

        public void RunInstallScript (string dir)
        {
            var installScript = dir
                + Path.DirectorySeparatorChar
                + "scripts"
                + Path.DirectorySeparatorChar
                + "Install.cs";

            Console.WriteLine ("Install script:");
            Console.WriteLine (installScript);

            if (File.Exists (installScript)) {
                Console.WriteLine ("Running install script...");
                Console.WriteLine ("");
                Script.StartFile (installScript);
            } else {
                Console.WriteLine ("No install script found. Skipping.");
            }
        }

        public string GetPackagePath(string packageName, string groupName, string externalRepositoryPath)
        {
            var file = String.Empty;

            var groupDir = externalRepositoryPath
                + Path.DirectorySeparatorChar
                    + groupName;

            var packageDir = groupDir
                + Path.DirectorySeparatorChar
                    + packageName;
           
            file = GetNewestFile(packageDir);
           
            return file;
        }

        public string GetPackagePath(string packageName, string externalRepositoryPath)
        {
            var file = String.Empty;

            foreach (var groupDir in Directory.GetDirectories(externalRepositoryPath)) {
                foreach (var packageDir in Directory.GetDirectories (groupDir))
                {
                    var name = Path.GetFileName(packageDir);

                    if (name.ToLower().Equals(packageName.ToLower()))
                    {
                        file = GetNewestFile(packageDir);
                        break;
                    }
                }
            }

            return file;
        }

        // TODO: This is a duplicate of Script.GetNewestFile. Move it to a component that can be shared
        public string GetNewestFile(string directory)
        {
            string file = String.Empty;

            var files = new DirectoryInfo(directory).GetFiles().OrderByDescending(p => p.CreationTime);

            foreach (var f in files)
            {
                file = f.FullName;
                break;
            }

            return file;
        }

    }
}

