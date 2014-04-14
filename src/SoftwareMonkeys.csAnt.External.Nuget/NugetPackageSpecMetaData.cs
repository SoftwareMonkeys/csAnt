using System;
using System.Collections.Generic;
using System.Xml.Serialization;


namespace SoftwareMonkeys.csAnt.External.Nuget
{
    [Serializable]
    [XmlType("metadata")]
    public class NugetPackageSpecMetaData
    {
        [XmlElement("id")]
        public string ID { get;set; }

        [XmlElement("version")]
        public string Version { get;set; }

        [XmlElement("authors")]
        public string Authors { get;set; }

        [XmlElement("owners")]
        public string Owners { get;set; }

        [XmlElement("projectUrl")]
        public string ProjectUrl { get;set; }

        [XmlElement("licenseUrl")]
        public string LicenceUrl { get;set; }

        [XmlElement("description")]
        public string Description { get;set; }

        [XmlElement("releaseNotes")]
        public string ReleaseNotes { get;set; }

        [XmlElement("copyright")]
        public string Copyright { get;set; }

        [XmlElement("tags")]
        public string Tags { get;set; }

        [XmlArray("dependencies")]
        [XmlArrayItem("dependency")]
        public List<NugetPackageDependency> Dependencies  { get;set; }

        public NugetPackageSpecMetaData ()
        {
            Construct();
        }

        public void Construct()
        {
            Dependencies = new List<NugetPackageDependency>();
            Version = "0.0.0.0";
            Description = "[No description has been provided.]";
            Authors = "[Not specified]";
            Owners = "[Not specified]";
        }
    }
}

