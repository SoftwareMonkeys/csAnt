using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SoftwareMonkeys.csAnt.Packages.Schema
{
    [XmlType("Events")]
    public class PackageEventInfoCollection : List<PackageEventInfo>
    {
        public PackageEventInfoCollection ()
        {
        }
    }
}

