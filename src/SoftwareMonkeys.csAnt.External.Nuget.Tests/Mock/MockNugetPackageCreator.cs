using System;
using System.IO;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt.External.Nuget.Tests
{
    public class MockNugetPackageCreator
    {
        public NugetPacker Packer { get;set; }

        public MockNugetPackageCreator ()
        {
            Packer = new NugetPacker();
        }

        public void Create(string name, Version version, string status, string branch)
        {
            var spec = new NugetPackageSpec();
            spec.MetaData.ID = name;
            spec.MetaData.Version = version.ToString();

            if (!String.IsNullOrEmpty(status))
                spec.MetaData.Version += "-" + status + "-" + branch;

            var helloWorldFile = "HelloWorld.txt";

            File.WriteAllText(
                PathConverter.ToAbsolute(helloWorldFile),
                "Hello world"
                );

            spec.AddFiles(helloWorldFile);

            spec.Save();

            Packer.PackageFile(spec.FilePath);
        }
    }
}

