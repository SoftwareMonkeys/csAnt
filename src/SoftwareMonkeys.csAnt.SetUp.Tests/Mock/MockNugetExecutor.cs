using System;
using SoftwareMonkeys.csAnt.External.Nuget;
using SoftwareMonkeys.csAnt.IO;
using System.IO;
using SoftwareMonkeys.csAnt.Versions;


namespace SoftwareMonkeys.csAnt.SetUp.Common.Tests
{
    public class MockNugetExecutor : NugetExecutor
    {
        public string OriginalDirectory { get;set; }

        public string WorkingDirectory { get;set; }
        
        public string ProjectName { get;set; }

        public string Version { get;set; }

        public MockNugetExecutor (
            string originalDirectory,
            string workingDirectory,
            string version
        )
        {
            ProjectName = "csAnt";
            OriginalDirectory = originalDirectory;
            WorkingDirectory = workingDirectory;
            Version = version;
        }

        public override void Execute(params string[] parameters)
        {
            var toDir = WorkingDirectory
                + Path.DirectorySeparatorChar
                    + "lib"
                    + Path.DirectorySeparatorChar
                    + "csAnt."
                    + Version;

            new FilesGrabber(
                OriginalDirectory,
                toDir
                ).GrabOriginalFiles();
        }
    }
}

