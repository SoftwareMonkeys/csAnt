using System;
using System.Collections.Generic;

namespace SoftwareMonkeys.csAnt.Packages.Schema
{
    public interface IPackageInfoCollection : IList<IPackageInfo>
    {
        void Add(IPackageInfo package);

        bool Contains(string query);
    }
}

