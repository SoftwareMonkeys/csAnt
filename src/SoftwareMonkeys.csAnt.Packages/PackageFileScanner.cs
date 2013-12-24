using System;
using SoftwareMonkeys.csAnt.Packages.Schema;
using System.IO;
using System.Collections.Generic;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt.Packages
{
    public class PackageFileScanner
    {
        public IFileFinder FileFinder { get; set; }

        public PackageFileScanner (
            IFileFinder fileFinder
        )
        {
            FileFinder = fileFinder;
        }

        public PackageFileInfo[] Scan (string packagesDirectory, params string[] filePatterns)
        {
            List<PackageFileInfo> files = new List<PackageFileInfo>();

            if (Directory.Exists (packagesDirectory)) {
                foreach (string file in FileFinder.FindFiles(packagesDirectory, filePatterns)) {
                    var fileInfo = new PackageFileInfo (file.TrimStart(Path.DirectorySeparatorChar));

                    files.Add (fileInfo);
                }
            }

            return files.ToArray();
        }
    }
}

