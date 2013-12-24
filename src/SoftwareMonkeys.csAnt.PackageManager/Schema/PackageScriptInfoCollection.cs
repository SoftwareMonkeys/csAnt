using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SoftwareMonkeys.csAnt.PackageManager
{
    [XmlType("Scripts")]
    public class PackageScriptInfoCollection : List<PackageScriptInfo>
    {
        public PackageScriptInfoCollection ()
        {
        }
    }
}

