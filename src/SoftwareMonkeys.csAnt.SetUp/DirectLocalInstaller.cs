using System;
using SoftwareMonkeys.csAnt.IO;


namespace SoftwareMonkeys.csAnt.SetUp
{
    public class DirectLocalInstaller : BaseInstaller
    {
        public string SourcePath { get;set; }

        public string DestinationPath { get;set; }

        public DirectLocalInstaller (string sourcePath, string destinationPath)
        {
            SourcePath = sourcePath;
            DestinationPath = destinationPath;
        }

        public override void Install ()
        {
            new FilesGrabber(
                SourcePath,
                DestinationPath
                ).GrabInstallation();
        }
    }
}

