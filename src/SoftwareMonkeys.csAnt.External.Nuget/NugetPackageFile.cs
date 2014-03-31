using System;
using System.Xml.Serialization;


namespace SoftwareMonkeys.csAnt.External.Nuget
{
    [Serializable]
    [XmlType("file")]
    public class NugetPackageFile
    {
        [XmlAttribute("src")]
        public string Src { get;set; }

        [XmlAttribute("target")]
        public string Target { get;set; }

        public NugetPackageFile ()
        {
        }
    }
}

