using System;

namespace SoftwareMonkeys.csAnt.Packages
{
    public class PackageFileRemover
    {
        public LocalPackageLoader Loader { get; set; }

        public PackageFileRemover ()
        {
            Construct();
        }

        public void Construct()
        {
            Loader = new LocalPackageLoader();
        }

        public void Construct (
            LocalPackageLoader loader
        )
        {
            Loader = loader;
        }

        public void RemoveFrom (string packagesDirectory, string packageName, string groupName, params string[] filePatterns)
        {
            throw new NotImplementedException();
/*            var pkg = Loader.Load(packageName);

            foreach (
*/
        }
    }
}

