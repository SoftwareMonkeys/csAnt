using System;
using SoftwareMonkeys.csAnt.Packages.Schema;
using System.IO;
using System.Xml.Serialization;

namespace SoftwareMonkeys.csAnt.Packages
{
    public class LocalPackageLoader
    {
        public LocalPackageLoader ()
        {
        }
        
        public PackageInfo Load (string packagesDirectory, string packageName, string groupName)
        {
            var dir = packagesDirectory
                + Path.DirectorySeparatorChar
                    + groupName
                    + Path.DirectorySeparatorChar
                    + packageName;

            return LoadFromDirectory(dir);
        }

        public PackageInfo Load (string packagesDirectory, string packageName)
        {
            PackageInfo package = null;

            foreach (var dir in Directory.GetDirectories(packagesDirectory)) {
                var name = Path.GetFileName (dir);

                if (name == packageName) {
                    package = LoadFromDirectory (dir);
                    break;
                }
            }

            return package;
        }

        public PackageInfo LoadFromDirectory (string directory)
        {
            var filePath = directory
                + Path.DirectorySeparatorChar
                    + Path.GetFileName(directory)
                    + ".pkg";

            return LoadFile (filePath);
        }

        public PackageInfo LoadFile (string filePath)
        {
            PackageInfo package = null;

            Console.WriteLine ("Loading package info file:");
            Console.WriteLine (filePath);

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

