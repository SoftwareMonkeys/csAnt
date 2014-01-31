using System;
using System.IO;
using System.Xml.Serialization;
using SoftwareMonkeys.csAnt.Packages.Schema;

namespace SoftwareMonkeys.csAnt.Packages
{
    public class LocalRepositoryLoader
    {
        public LocalPackageLoader PackageLoader { get;set; }

        public RepositoryFileNamer FileNamer { get; set; }

        public LocalRepositoryLoader (
            RepositoryFileNamer fileNamer,
            LocalPackageLoader packageLoader
        )
        {
            PackageLoader = packageLoader;
            FileNamer = fileNamer;
        }

        public LocalRepositoryLoader ()
        {
            PackageLoader = new LocalPackageLoader();
            FileNamer = new RepositoryFileNamer();
        }

        public RepositoryInfo Load (string repositoryPath)
        {
            if (!Directory.Exists (repositoryPath))
                throw new ArgumentException ("The repository directory was not found.", "repositoryPath");

            var filePath = FileNamer.GetRepositoryFile(repositoryPath);

            var repository = LoadLocalFile(filePath);

            foreach (var groupDir in Directory.GetDirectories(repositoryPath)) {
                var groupName = Path.GetDirectoryName(groupDir);

                foreach (var packageName in Directory.GetDirectories(groupDir))
                {
                    var package = PackageLoader.Load(repositoryPath, packageName, groupName);

                    repository.Packages.Add (package);
                }
            }

            repository.FilePath = filePath;

            repository.Path = repositoryPath;

            return repository;
        }
        
        public RepositoryInfo LoadLocalFile (string filePath)
        {
            RepositoryInfo package = null;

            if (File.Exists (filePath)) {
                using (StreamReader reader = new StreamReader(File.OpenRead(filePath))) {
                    var serializer = new XmlSerializer (typeof(RepositoryInfo));
                    package = (RepositoryInfo)serializer.Deserialize (reader);
                }
            }

            return package;
        }
    }
}

