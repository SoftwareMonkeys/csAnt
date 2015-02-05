using System;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.csAnt.SetUp.Repack;
using System.IO;
using SoftwareMonkeys.csAnt.External.Nuget.Tests.Mock;
using SoftwareMonkeys.csAnt.Tests.Helpers;

namespace SoftwareMonkeys.csAnt.SetUp.Tests
{
    public class SetUpTestPreparer
    {
        public string OriginalDirectory { get;set; }

        public string WorkingDirectory { get;set; }

        public string LocalNugetFilePath { get; set; }

        public TestInstallationCreator TestInstallation { get; set; }

        public MockNugetFeedCreator FeedCreator { get;set; }

        public SetUpTestPreparer (
            string originalDirectory,
            string workingDirectory
            )
        {
            OriginalDirectory = originalDirectory;
            WorkingDirectory = workingDirectory;
        }
        
        public string Prepare(string setupFileName, string buildMode)
        {
            // Grab all the original files to the staging directory
            new FilesGrabber(
                OriginalDirectory,
                WorkingDirectory,
                true
                ).GrabOriginalFiles();

            // TODO: Is this the best way to create nodes? Is this needed?
            new ProjectNodeCreator().CreateGroupNode();

            // Build the solution
            new SolutionBuilder(buildMode).BuildSolution("csAnt.Tests");

            // Repack the csAnt-SetUp.exe file to include dependencies
            new SetUpRepacker(buildMode).Repack();

            // Create a test project directory
            TestInstallation = new TestInstallationCreator (OriginalDirectory, WorkingDirectory);
            var testProjectDirectory = TestInstallation.InstallationPath;

            var workingBinDirectory = WorkingDirectory
                + Path.DirectorySeparatorChar
                    + "bin"
                    + Path.DirectorySeparatorChar
                    + buildMode
                    + Path.DirectorySeparatorChar
                    + "packed";

            DirectoryChecker.EnsureDirectoryExists(testProjectDirectory);

            // Copy setup file to test project directory, ready to run
            new FilesGrabber(
                workingBinDirectory,
                testProjectDirectory
                ).GrabOriginalFiles(
                setupFileName
                );

//            CreateMockFeed();
            FeedCreator = new MockNugetFeedCreator (OriginalDirectory, WorkingDirectory);
            FeedCreator.Create ();

            // Create the path to a local copy of nuget.exe (so it doesn't get downloaded from the web)
            LocalNugetFilePath = WorkingDirectory
                + Path.DirectorySeparatorChar
                    + "lib"
                    + Path.DirectorySeparatorChar
                    + "nuget.exe";

            return testProjectDirectory;
        }
    }
}

