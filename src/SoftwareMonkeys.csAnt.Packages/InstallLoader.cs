using System;
using SoftwareMonkeys.csAnt.Packages.Schema;
using System.IO;
using System.Xml.Serialization;
using SoftwareMonkeys.csAnt.Packages;

namespace SoftwareMonkeys.csAnt.Packages
{
    public class InstallLoader
    {
        public InstallLoader ()
        {
        }

        public InstallInfo Load (string workingDirectory, string installName)
        {
            var installDir = workingDirectory
                + Path.DirectorySeparatorChar
                + "install";

            InstallInfo install = null;

            foreach (var dir in Directory.GetDirectories(installDir)) {
                var name = Path.GetFileName (dir);

                if (name == installName) {
                    install = LoadDirectory (dir);
                    break;
                }
            }

            return install;
        }

        public InstallInfo LoadDirectory (string directory)
        {
            var filePath = directory
                + Path.DirectorySeparatorChar
                    + Path.GetFileName(directory)
                    + ".install";

            return LoadFile (filePath);
        }

        public InstallInfo LoadFile (string filePath)
        {
            InstallInfo install = null;

            if (File.Exists (filePath)) {
                using (StreamReader reader = new StreamReader(File.OpenRead(filePath))) {
                    var serializer = new XmlSerializer (typeof(InstallInfo));
                    install = (InstallInfo)serializer.Deserialize (reader);
                }
            }

            return install;
        }
    }
}

