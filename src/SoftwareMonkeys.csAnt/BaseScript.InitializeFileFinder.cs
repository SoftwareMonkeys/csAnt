using System;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        public void InitializeFileFinder(IFileFinder finder)
        {
            fileFinder = finder;
        }
    }
}

