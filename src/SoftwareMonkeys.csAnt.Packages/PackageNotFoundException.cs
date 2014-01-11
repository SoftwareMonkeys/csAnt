using System;

namespace SoftwareMonkeys.csAnt.Packages
{
    public class PackageNotFoundException : Exception
    {
        public PackageNotFoundException (string packageName) : base("The '" + packageName + "' package does not exist. Please create it first.")
        {
        }
    }
}

