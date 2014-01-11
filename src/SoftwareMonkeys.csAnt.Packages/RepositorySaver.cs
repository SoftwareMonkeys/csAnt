using System;
using SoftwareMonkeys.csAnt.Packages.Schema;
using System.IO;
using System.Xml.Serialization;

namespace SoftwareMonkeys.csAnt.Packages
{
    public class RepositorySaver
    {
        public RepositoryFileNamer FileNamer { get;set; }

        public RepositorySaver (
            RepositoryFileNamer fileNamer
            )
        {
            FileNamer = fileNamer;
        }

        public string Save (IRepositoryInfo repository)
        {
            Console.WriteLine ("");
            Console.WriteLine ("Saving repository...");
            Console.WriteLine ("");

            if (String.IsNullOrEmpty (repository.Path))
                throw new ArgumentException ("The repository.Path property must be set.", "repository.Path");

            if (String.IsNullOrEmpty (repository.FilePath)) {
                repository.FilePath = FileNamer.CreateFilePath(repository.Name, repository.Path);
            }

            Console.WriteLine ("File path:");
            Console.WriteLine (repository.FilePath);

            Console.WriteLine ("Path:");
            Console.WriteLine (repository.Path);

            if (!Directory.Exists(repository.Path))
                Directory.CreateDirectory(repository.Path);

            using (StreamWriter writer = File.CreateText(repository.FilePath))
            {
                var extraTypes = new Type[]{
                    typeof(PackageInfo)
                };

                var serializer = new XmlSerializer(repository.GetType(), extraTypes);
                serializer.Serialize(writer, repository);
            }

            return repository.FilePath;
        }
    }
}

