using System;
using System.Xml;
using System.Xml.Serialization;


namespace SoftwareMonkeys.csAnt.External.Nuget
{
    [Serializable]
    [XmlType("dependency")]
    public class NugetPackageDependency
    {
        [XmlAttribute("id")]
        public string ID { get;set; }

        [XmlAttribute("version")]
        public string Version { get;set; }

        public NugetPackageDependency ()
        {
        }
    }
}

