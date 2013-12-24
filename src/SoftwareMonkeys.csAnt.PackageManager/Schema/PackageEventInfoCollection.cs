using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SoftwareMonkeys.csAnt.PackageManager
{
    [XmlType("Events")]
    public class PackageEventInfoCollection : List<PackageEventInfo>
    {
        public PackageEventInfoCollection ()
        {
        }
    }
}

