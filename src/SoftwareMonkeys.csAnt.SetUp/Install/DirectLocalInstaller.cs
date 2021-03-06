using System;
using SoftwareMonkeys.csAnt.IO;


namespace SoftwareMonkeys.csAnt.SetUp.Install
{
    /// <summary>
    /// The direct local installer bypasses the standard install system and copies files directly
    /// from the source directory to the final destination in the destination.
    /// </summary>
    public class DirectLocalInstaller : Installer
    {
        public string SourcePath { get;set; }

        public DirectLocalInstaller (string sourcePath, string destinationPath)
        {
            SourcePath = sourcePath;
            DestinationPath = destinationPath;
        }

        public DirectLocalInstaller (string sourcePath, string destinationPath, bool overwrite)
        {
            SourcePath = sourcePath;
            DestinationPath = destinationPath;
            Overwrite = true;
        }

        public override void Install ()
        {
            var grabber = new FilesGrabber(
                SourcePath,
                DestinationPath,
                Overwrite
                );

            grabber.GrabInstallation();
        }
    }
}

