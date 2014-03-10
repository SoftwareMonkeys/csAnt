using System;
using System.IO;


namespace SoftwareMonkeys.csAnt.IO
{
    static public class DirectoryChecker
    {
        static public void EnsureDirectoryExists(
            string directoryPath
        )
        {
            // Check parent directories, up to the last one
            if (directoryPath.IndexOf(Path.DirectorySeparatorChar) != directoryPath.LastIndexOf(Path.DirectorySeparatorChar))
                EnsureDirectoryExists(Path.GetDirectoryName(directoryPath));

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
        }
    }
}

