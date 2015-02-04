using System;
using NUnit.Framework;
using SoftwareMonkeys.csAnt.IO;
using System.IO;
using SoftwareMonkeys.csAnt.Processes;
using SoftwareMonkeys.csAnt.External.Nuget;
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.csAnt.SetUp;
using SoftwareMonkeys.csAnt.SourceControl.Git;
using SoftwareMonkeys.csAnt.External.Nuget.Tests.Mock;
using SoftwareMonkeys.csAnt.SetUp.Repack;
using SoftwareMonkeys.csAnt.Versions;
using SoftwareMonkeys.csAnt.SetUp.Tests;
using SoftwareMonkeys.csAnt.Tests;


namespace SoftwareMonkeys.csAnt.SetUpFromWebConsole.Tests.Integration
{
    [TestFixture]
    public class SetUpConsoleIntegrationTestFixture : BaseTestFixture
    {
        public SetUpTestPreparer Preparer { get; set; }

        [Test]
        public void Test_SetUp()
        {
            var buildMode = "Release";
#if DEBUG
            buildMode = "Debug";
#endif

            Console.WriteLine("");
            Console.WriteLine("Testing setup console...");
            Console.WriteLine("");
            Console.WriteLine("Build mode: " + buildMode);

            var setupFileName = "csAnt-SetUp.exe";

            // Prepare the test and place the setup file in the test project directory
            var testProjectDirectory = Prepare(setupFileName, buildMode);

            // Move to the test project directory
            Environment.CurrentDirectory = testProjectDirectory;

            // Launch the setup file with a local nuget
            var processStarter = new DotNetProcessStarter();
            processStarter.Start(
                setupFileName,
                "-nuget=" + Preparer.LocalNugetFilePath,
                "-source=" + Preparer.FeedCreator.FeedPath
            );

            Assert.IsFalse(processStarter.IsError, "An error occurred.");
        }

        // TODO: Remove this whole function if not needed and embed content directly
        public string Prepare(string setupFileName, string buildMode)
        {
            Preparer = new SetUpTestPreparer (OriginalDirectory, WorkingDirectory);
            return Preparer.Prepare (setupFileName, buildMode);

            // TODO: Remove if not needed
            /*// Grab all the original files to the staging directory
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
            var testProjectDirectory = CreateTestProjectPath();

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

            CreateMockFeed();

            // Create the path to a local copy of nuget.exe (so it doesn't get downloaded from the web)
            LocalNugetFilePath = WorkingDirectory
                + Path.DirectorySeparatorChar
                    + "lib"
                    + Path.DirectorySeparatorChar
                    + "nuget.exe";

            return testProjectDirectory;*/
        }


        // TODO: Remove if not needed
        /*public string CreateMockFeed()
        {
            var creator = new MockNugetFeedCreator (OriginalDirectory, CurrentDirectory);
            creator.Create();

            MockFeedPath = creator.FeedPath;

            return creator.FeedPath;
        }*/
    }
}

