using System;
using System.IO;
using System.Linq;

namespace SoftwareMonkeys.csAnt.Packages
{
    public class PackageRetriever
    {
        public PackageRetriever ()
        {}
        
        public void Pull (
            string workingDirectory,
            string packagesDirectory,
            string packageName,
            string groupName,
            string externalRepositoryPath
            )
        {
            var file = GetPackagePath(packageName, groupName, externalRepositoryPath);

            var toFile = file.Replace(
                externalRepositoryPath,
                packagesDirectory
            );

            if (!Directory.Exists(Path.GetDirectoryName(toFile)))
                Directory.CreateDirectory(Path.GetDirectoryName(toFile));

            File.Copy(file, toFile, true);
        }

        public void Pull (
            string workingDirectory,
            string packagesDirectory,
            string packageName,
            string remoteRepositoryPath
            )
        {
            var file = GetPackagePath(packageName, remoteRepositoryPath);

            var toFile = file.Replace(
                remoteRepositoryPath,
                packagesDirectory
            );
            
            if (!Directory.Exists(Path.GetDirectoryName(toFile)))
                Directory.CreateDirectory(Path.GetDirectoryName(toFile));

            File.Copy(file, toFile, true);
        }

        public string GetPackagePath(string packageName, string groupName, string remoteRepositoryPath)
        {
            var file = String.Empty;

            var groupDir = remoteRepositoryPath
                + Path.DirectorySeparatorChar
                    + groupName;

            var packageDir = groupDir
                + Path.DirectorySeparatorChar
                    + packageName;
           
            file = GetNewestFile(packageDir);
           
            return file;
        }

        public string GetPackagePath(string packageName, string remoteRepositoryPath)
        {
            var file = String.Empty;

            foreach (var groupDir in Directory.GetDirectories(remoteRepositoryPath)) {
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

