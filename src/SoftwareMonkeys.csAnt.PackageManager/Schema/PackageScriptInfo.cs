using System;
using System.Xml.Serialization;

namespace SoftwareMonkeys.csAnt.PackageManager
{
    [XmlType("Script")]
    public class PackageScriptInfo
    {
        public string Name { get; set; }

        public string Arguments { get;set; }

        public PackageScriptInfo()
        {}

        public PackageScriptInfo (string name)
        {
            Name = name;
        }

        public PackageScriptInfo (string name, string arguments)
        {
            Name = name;
            Arguments = arguments;
        }
    }
}

