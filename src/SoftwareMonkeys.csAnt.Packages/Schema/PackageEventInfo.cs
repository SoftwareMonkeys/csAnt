using System;
using System.Xml.Serialization;

namespace SoftwareMonkeys.csAnt.Packages.Schema
{
    [XmlType("Event")]
    public class PackageEventInfo
    {
        public string Name { get;set; }

        [XmlArray("Scripts")]
        public PackageScriptInfoCollection Scripts { get; set; }

        public PackageEventInfo ()
        {
        }

        public PackageEventInfo (string name, params PackageScriptInfo[] scripts)
        {
            Scripts = new PackageScriptInfoCollection();
            Scripts.AddRange(scripts);
        }
    }
}

