using System;
using System.IO;
using System.Xml.Serialization;


namespace SoftwareMonkeys.csAnt.External.Nuget
{
    public class NugetPackageSpecSaver
    {
        public NugetPackageSpecSaver ()
        {
        }

        public void Save(NugetPackageSpec spec, string filePath)
        {
            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            using (var writer = File.CreateText(filePath))
            {
                var serializer = new XmlSerializer(spec.GetType());
                serializer.Serialize(writer, spec);
            }
        }
    }
}

