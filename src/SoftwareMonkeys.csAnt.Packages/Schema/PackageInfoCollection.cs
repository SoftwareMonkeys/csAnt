using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace SoftwareMonkeys.csAnt.Packages.Schema
{
    [XmlInclude(typeof(PackageInfo))]
    public class PackageInfoCollection : List<IPackageInfo>, IPackageInfoCollection
    {
        public PackageInfoCollection ()
        {
        }

        public new void Add(IPackageInfo package)
        {
            base.Add(package);
        }

        public bool Contains(string name)
        {
            var repo = from r in ToArray ()
                    where r.Name == name
                    select r;

            return repo != null;
        }
    }
}

