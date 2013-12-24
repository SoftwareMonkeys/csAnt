using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SoftwareMonkeys.csAnt.Packages.Schema;

namespace SoftwareMonkeys.csAnt.Packages
{
    [Serializable]
    public class InstallInfo : IInstallInfo
    {
        public string Name { get;set; }

        [XmlArray("Packages")]
        [XmlArrayItem("Package")]
        public List<string> Packages { get;set; }

        public InstallInfo()
        {
            Packages = new List<string>();
        }

        public InstallInfo (string name)
        {
            Packages = new List<string>();
            Name = name;
        }
    }
}

