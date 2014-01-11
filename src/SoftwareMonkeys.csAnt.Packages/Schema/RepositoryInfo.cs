using System;
using System.Xml.Serialization;

namespace SoftwareMonkeys.csAnt.Packages.Schema
{
    public class RepositoryInfo : IRepositoryInfo
    {
        public string Name { get;set; }

        [XmlArray("Packages")]
        [XmlArrayItem("Package", typeof(PackageInfo))]
        public IPackageInfoCollection Packages { get; set; }

        [XmlIgnore]
        public string Path { get;set; }

        [XmlIgnore]
        public string FilePath { get;set; }

        public RepositoryInfo ()
        {
        }

        public RepositoryInfo(string name)
        {
            Name = name;
        }

        public RepositoryInfo(string name, string path)
        {
            Name = name;
            Path = path;
        }
    }
}

