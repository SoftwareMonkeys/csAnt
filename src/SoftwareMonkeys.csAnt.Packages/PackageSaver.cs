using System;
using SoftwareMonkeys.csAnt.Packages.Schema;
using System.IO;
using System.Xml.Serialization;

namespace SoftwareMonkeys.csAnt.Packages
{
    public class PackageSaver
    {
        public PackageInfoFileNamer FileNamer { get; set; }

        public PackageSaver (
            PackageInfoFileNamer fileNamer
        )
        {
            FileNamer = fileNamer;
        }
        
        public PackageSaver ()
        {
            FileNamer = new PackageInfoFileNamer();
        }

        public string Save(string workingDirectory, PackageInfo package)
        {
            Console.WriteLine ("");
            Console.WriteLine ("Saving package...");
            Console.WriteLine ("");

            var filePath = FileNamer.CreateInfoFilePath(workingDirectory, package.Name, package.GroupName);

            Console.WriteLine ("File path:");
            Console.WriteLine (filePath);

            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            using (StreamWriter writer = File.CreateText(filePath))
            {
                var serializer = new XmlSerializer(package.GetType());
                serializer.Serialize(writer, package);
            }

            return filePath;
        }
    }
}

