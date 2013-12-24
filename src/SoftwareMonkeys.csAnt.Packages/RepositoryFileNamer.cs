using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Packages
{
    public class RepositoryFileNamer
    {
        public string Extension = ".repo";

        public RepositoryFileNamer ()
        {
        }

        public string CreateFilePath (string name, string repositoryPath)
        {
            return repositoryPath
                + Path.DirectorySeparatorChar
                    + CreateFileName(name);
        }

        public string CreateFileName (string name)
        {
            return name
                + Extension;
        }

        public string GetRepositoryFile(string repositoryPath)
        {
            var files = Directory.GetFiles(repositoryPath, "*.repo");

            if (files.Length == 0)
                throw new ArgumentException("Cannot find a *.repo file at the specified repository path.");

            if (files.Length > 1)
                throw new InvalidOperationException("Multiple *.repo files found at the specified repository path.");

            return files[0];
        }
    }
}

