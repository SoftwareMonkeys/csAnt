using System;
using SoftwareMonkeys.csAnt.PackageManager.Schema;
using System.IO;

namespace SoftwareMonkeys.csAnt.PackageManager
{
    public class FileScanner
    {
        public PackageInfo Package { get; set; }

        public FileScanner (PackageInfo pkg)
        {
            Package = pkg;
        }

        public void Scan ()
        {
            foreach (string file in Directory.GetFiles(Package.WorkingDirectory, "*", SearchOption.AllDirectories)) {
                var fileInfo = new PackageFileInfo(file);

                Package.Files.Add(fileInfo);
            }
        }
    }
}

