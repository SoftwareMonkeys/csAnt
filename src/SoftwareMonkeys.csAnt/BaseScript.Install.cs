using System;
using SoftwareMonkeys.csAnt.InstallConsole;
using System.IO;
using System.Collections.Generic;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        public void Install (string name)
        {
            Install(name, false);
        }

        public void Install(string name, bool overwriteFiles)
        {
            InstallTo (name, CurrentDirectory, overwriteFiles);
        }
    }
}

