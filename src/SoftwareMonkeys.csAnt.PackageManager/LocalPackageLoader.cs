using System;
using SoftwareMonkeys.csAnt.PackageManager.Schema;
using System.IO;
using System.Xml.Serialization;

namespace SoftwareMonkeys.csAnt.PackageManager
{
    public class PackageLoader
    {
        public PackageLoader ()
        {
        }
        
        public PackageInfo LoadLocal (string repositoryPath, string groupName, string packageName)
        {
            PackageInfo package = null;

            var dir = repositoryPath
                + Path.DirectorySeparatorChar
                    + groupName
                    + Path.DirectorySeparatorChar
                    + packageName;

            return LoadLocalDirectory(dir);
        }

        public PackageInfo LoadLocal (string repositoryPath, string packageName)
        {
            PackageInfo package = null;

            foreach (var dir in Directory.GetDirectories(repositoryPath)) {
                var name = Path.GetFileName (dir);

                if (name == packageName) {
                    package = LoadLocalDirectory (dir);
                    break;
                }
            }

            return package;
        }

        public PackageInfo LoadLocalDirectory (string directory)
        {
            var filePath = directory
                + Path.DirectorySeparatorChar
                    + Path.GetFileName(directory)
                    + ".pkg";

            return LoadLocalFile (filePath);
        }

        public PackageInfo LoadLocalFile (string filePath)
        {
            PackageInfo package = null;

            if (File.Exists (filePath)) {
                using (StreamReader reader = new StreamReader(File.OpenRead(filePath))) {
                    var serializer = new XmlSerializer (typeof(PackageInfo));
                    package = (PackageInfo)serializer.Deserialize (reader);
                }
            }

            return package;
        }
    }
}

