using System;
using System.Collections.Generic;

namespace SoftwareMonkeys.csAnt.Packages.Schema
{
    public interface IInstallInfo
    {
        string Name { get;set; }

        List<string> Packages { get;set; }
    }
}

