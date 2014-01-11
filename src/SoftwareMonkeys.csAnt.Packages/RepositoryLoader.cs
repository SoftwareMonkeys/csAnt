using System;
using SoftwareMonkeys.csAnt.Packages.Schema;

namespace SoftwareMonkeys.csAnt.Packages
{
    public class RepositoryLoader
    {
        public LocalRepositoryLoader LocalLoader { get;set; }

        public HttpRepositoryLoader HttpLoader { get;set; }

        public RepositoryLoader (
            LocalRepositoryLoader localLoader,
            HttpRepositoryLoader httpLoader
        )
        {
            LocalLoader = localLoader;
            HttpLoader = httpLoader;
        }

        public RepositoryLoader ()
        {
            LocalLoader = new LocalRepositoryLoader();
            HttpLoader = new HttpRepositoryLoader();
        }

        public RepositoryInfo Load(string repositoryPath)
        {
            if (HttpUtility.IsHttpPath(repositoryPath))
                return HttpLoader.Load(repositoryPath);
            else
                return LocalLoader.Load(repositoryPath);
        }
    }
}

