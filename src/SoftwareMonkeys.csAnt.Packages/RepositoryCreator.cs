using System;
using SoftwareMonkeys.csAnt.Packages.Schema;

namespace SoftwareMonkeys.csAnt.Packages
{
    public class RepositoryCreator
    {
        public RepositoryFileNamer FileNamer { get;set; }

        public RepositoryCreator (
            RepositoryFileNamer fileNamer
            )
        {
            FileNamer = fileNamer;
        }

        public IRepositoryInfo Create(string name)
        {
            return new RepositoryInfo(name);
        }

        public IRepositoryInfo Create(string name, string path)
        {
            var repo = new RepositoryInfo(name, path);

            repo.FilePath = FileNamer.CreateFilePath(name, path);

            return repo;
        }
    }
}

