using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SoftwareMonkeys.csAnt.Packages.Schema
{
    [XmlType("Scripts")]
    public class PackageScriptInfoCollection : List<PackageScriptInfo>
    {
        public PackageScriptInfoCollection ()
        {
        }
    }
}

