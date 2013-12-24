using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Packages.Tests
{
    static public class PackagesTestUtilities
    {
        static public string GetLocalRepositoryPath (string workingDirectory)
        {
            var repoPath = "";

            var p = workingDirectory;

            while (true) {
                // If the path doesn't contain a slash, or only contains one, break out of the loop
                if (
                    !p.Contains (Path.DirectorySeparatorChar.ToString())
                    || p.LastIndexOf (Path.DirectorySeparatorChar) == p.IndexOf (Path.DirectorySeparatorChar)
                    )
                {
                    break;
                }

                p = Path.GetDirectoryName (p);

                var pkgPath = p
                    + Path.DirectorySeparatorChar
                    + "pkg";
                
                var projectsNodePath = p
                    + Path.DirectorySeparatorChar
                    + "Projects.node";

                if (Directory.Exists (pkgPath) && File.Exists (projectsNodePath)) {
                    repoPath = p;
                    break;
                }
            }

            return repoPath;
        }
    }
}

