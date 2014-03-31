using System;
using System.IO;
using SoftwareMonkeys.csAnt.IO;



namespace SoftwareMonkeys.csAnt.External.Nuget
{
    public class NugetPackageCreator
    {
        public NugetPackageCreator ()
        {
        }

        public NugetPackageSpec CreateFromRelease(string projectName, string releaseName)
        {
            var spec = new NugetPackageSpec(releaseName);

            var releaseListFile = Environment.CurrentDirectory
                + Path.DirectorySeparatorChar
                    + "rls"
                    + Path.DirectorySeparatorChar
                    + releaseName
                    + "-list.txt";

            var patterns = File.ReadAllLines(releaseListFile);
            
            // TODO: Move this object to a property to support dependency injection
            var files = new FileFinder().FindFiles(Environment.CurrentDirectory, patterns);

            spec.AddFiles(files);

            spec.FilePath = Environment.CurrentDirectory
                + Path.DirectorySeparatorChar
                    + "pkg"
                    + Path.DirectorySeparatorChar
                    + projectName
                    + "-"
                    + releaseName
                    + ".nuspec";
            
            // TODO: Move this object to a property to support dependency injection
            spec.Saver = new NugetPackageSpecSaver();

            return spec;

        }
    }
}

