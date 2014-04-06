using System;
using SoftwareMonkeys.csAnt.IO;


namespace SoftwareMonkeys.csAnt.SetUp.Common
{
    public class DirectInstaller : BaseInstaller
    {
        public string SourcePath { get;set; }

        public string DestinationPath { get;set; }

        public DirectInstaller (string sourcePath, string destinationPath)
        {
            SourcePath = sourcePath;
            DestinationPath = destinationPath;
        }

        public override void Install ()
        {
            new FilesGrabber(
                SourcePath,
                DestinationPath
                ).GrabInstallFiles();
        }
    }
}

