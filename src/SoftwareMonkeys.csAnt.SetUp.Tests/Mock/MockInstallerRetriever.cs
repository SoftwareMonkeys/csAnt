using System;
using System.IO;
using SoftwareMonkeys.csAnt.IO;


namespace SoftwareMonkeys.csAnt.SetUp.Tests
{
    public class MockInstallerRetriever : BaseInstallerRetriever
    {
        public string SourcePath { get;set; }

        public string DestinationPath { get;set; }

        public Version Version { get;set; }

        public MockInstallerRetriever(string source, string destination, Version version)
        {
            SourcePath = source;
            DestinationPath = destination;
            Version = version;
        }

        public override void Retrieve ()
        {
            var toDir = DestinationPath
                + Path.DirectorySeparatorChar
                    + "lib"
                    + Path.DirectorySeparatorChar
                    + "csAnt."
                    + Version;

            new FilesGrabber(
                SourcePath,
                toDir
                ).GrabOriginalFiles();
        }
    }
}

