using System;

namespace SoftwareMonkeys.csAnt
{
    public class PackageManagerNotInitializedException : Exception
    {
        public PackageManagerNotInitializedException () : base("The PackageManager has not been initialized so the Packages property is unavailable. Call InitializePackageManager(IPackageManager) first.")
        {
        }
    }
}

