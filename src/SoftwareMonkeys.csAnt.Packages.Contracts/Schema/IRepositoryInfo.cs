using System;
using SoftwareMonkeys.csAnt.Packages.Schema;

namespace SoftwareMonkeys.csAnt
{
    public interface IRepositoryInfo
    {
        string Name { get;set; }

        string Path { get;set; }

        string FilePath { get;set; }

        IPackageInfoCollection Packages { get;set; }
    }
}

