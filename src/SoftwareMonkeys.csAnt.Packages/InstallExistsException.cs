using System;

namespace SoftwareMonkeys.csAnt.Packages
{
    public class InstallExistsException : Exception
    {
        public InstallExistsException (string installName) : base("The '" + installName + "' already exists.")
        {
        }
    }
}

