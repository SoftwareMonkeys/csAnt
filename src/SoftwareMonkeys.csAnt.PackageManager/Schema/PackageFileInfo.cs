using System;
using System.Xml;
using System.Xml.Serialization;

namespace SoftwareMonkeys.csAnt.PackageManager.Schema
{
    [Serializable]
    [XmlType("File")]
    public class PackageFileInfo
    {
        [XmlAttribute]
        public string Name { get; set; }

        public PackageFileInfo()
        {}

        public PackageFileInfo (string name)
        {
            Name = name;
        }
    }
}

