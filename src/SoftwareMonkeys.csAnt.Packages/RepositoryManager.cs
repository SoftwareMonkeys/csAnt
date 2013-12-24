using System;
using SoftwareMonkeys.csAnt.Packages.Schema;

namespace SoftwareMonkeys.csAnt.Packages
{
    public class RepositoryManager : IRepositoryManager
    {
        public RepositoryCreator Creator { get; set; }

        public RepositorySaver Saver { get; set; }

        public RepositoryLoader Loader { get; set; }

        public RepositoryManager (
            RepositoryCreator creator,
            RepositorySaver saver,
            RepositoryLoader loader
        )
        {
            Creator = creator;
            Saver = saver;
            Loader = loader;
        }
        
        public RepositoryManager ()
        {
            var fileNamer = new RepositoryFileNamer();

            Creator = new RepositoryCreator(
                fileNamer
            );
            Saver = new RepositorySaver(
                fileNamer
            );
            Loader = new RepositoryLoader();
        }

        public IRepositoryInfo Create (string name, string path)
        {
            return Create (name, path, true);
        }

        public IRepositoryInfo Create(string name, string path, bool autoSave)
        {
            var repo = Creator.Create(name, path);

            if (autoSave)
                Saver.Save (repo);

            return repo;
        }

        public IRepositoryInfo Load(string path)
        {
            return Loader.Load (path);
        }

        public void Save(IRepositoryInfo repository)
        {
            Saver.Save (repository);
        }
    }
}

