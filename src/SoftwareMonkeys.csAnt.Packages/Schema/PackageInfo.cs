using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace SoftwareMonkeys.csAnt.Packages.Schema
{
    [Serializable]
    [XmlType("Package")]
    public class PackageInfo : IPackageInfo
    {
        public string Name { get; set; }

        public string GroupName { get;set; }

        [XmlArray("Files")]
        public PackageFileInfoCollection Files { get; set; }
        
        [XmlArray("Events")]
        public PackageEventInfoCollection Events { get; set; }

        public PackageInfo ()
        {
        }

        public PackageInfo (
            string name,
            string groupName
        )
        {
            Name = name;
            GroupName = groupName;
        }
    }
}

