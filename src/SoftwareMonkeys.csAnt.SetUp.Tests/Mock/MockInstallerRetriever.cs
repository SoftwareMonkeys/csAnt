using System;
using System.IO;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.SetUp.Install.Retrieve;


namespace SoftwareMonkeys.csAnt.SetUp.Tests
{
    public class MockInstallerRetriever : BaseInstallerRetriever
    {
        public string SourcePath { get;set; }

        public string DestinationPath { get;set; }

        public MockInstallerRetriever(string source, string destination)
        {
            SourcePath = source;
            DestinationPath = destination;
        }

        public override void Retrieve (string packageName)
        {
            throw new NotImplementedException ();
        }

        public override void Retrieve (string packageName, Version version, string status, string branch)
        {
            var toDir = DestinationPath
                + Path.DirectorySeparatorChar
                    + "lib"
                    + Path.DirectorySeparatorChar
                    + "csAnt."
                    + version;

            new FilesGrabber(
                SourcePath,
                toDir
                ).GrabOriginalFiles();
        }
    }
}

