using System;
using SoftwareMonkeys.csAnt.Packages.Schema;
using System.IO;
using System.Xml.Serialization;
using SoftwareMonkeys.csAnt.Packages;

namespace SoftwareMonkeys.csAnt.Packages
{
    public class InstallSaver
    {
        public InstallSaver ()
        {
        }

        public string Save(string workingDirectory, IInstallInfo install)
        {
            Console.WriteLine ("");
            Console.WriteLine ("Saving install...");
            Console.WriteLine ("");

                var filePath = workingDirectory
                    + Path.DirectorySeparatorChar
                    + "install"
                    + Path.DirectorySeparatorChar
                    + install.Name
                    + Path.DirectorySeparatorChar
                    + install.Name
                    + ".install";

            Console.WriteLine ("File path:");
            Console.WriteLine (filePath);

            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            using (StreamWriter writer = File.CreateText(filePath))
            {
                var serializer = new XmlSerializer(install.GetType());
                serializer.Serialize(writer, install);
            }

            return filePath;
        }
    }
}

