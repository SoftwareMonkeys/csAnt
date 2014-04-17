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


namespace SoftwareMonkeys.csAnt.SetUpFromWebConsole.Tests.Integration
{
    [TestFixture]
    public class SetUpConsoleIntegrationTestFixture : BaseSetUpConsoleIntegrationTestFixture
    {
        public string MockFeedPath { get;set; }
        public string LocalNugetFilePath { get;set; }

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
                "-nuget=" + LocalNugetFilePath,
                "-source=" + MockFeedPath
            );

            Assert.IsFalse(processStarter.IsError);
        }
        
        [Test]
        public void Test_SetUp_Import()
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
            var testProjectDirectory = PrepareImport(setupFileName, buildMode);

            // Set the import path to the current working directory
            var importPath = WorkingDirectory;

            // Move to the test project directory
            Environment.CurrentDirectory = testProjectDirectory;

            // Launch the setup file with a local nuget 
            var processStarter = new DotNetProcessStarter();
            processStarter.Start(
                setupFileName,
                "-nuget=" + LocalNugetFilePath,
                "-source=" + MockFeedPath,
                "-import=" + importPath
            );

            Assert.IsFalse(processStarter.IsError);
        }

        public string Prepare(string setupFileName, string buildMode)
        {
            // Grab all the original files
            new FilesGrabber(
                OriginalDirectory,
                WorkingDirectory
                ).GrabOriginalFiles();

            // TODO: Is this the best way to create nodes? Is this needed?
            new ProjectNodeCreator().CreateGroupNode();
            
            // Build the solution
            new SolutionBuilder().BuildSolution("csAnt");

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


            return testProjectDirectory;
        }
        
        public string PrepareImport(string setupFileName, string buildMode)
        {
            new Gitter().Clone(OriginalDirectory, CurrentDirectory);
            
            return Prepare(setupFileName, buildMode);
        }

        public string CreateTestProjectPath()
        {
            return Path.GetDirectoryName(WorkingDirectory)
                + Path.DirectorySeparatorChar
                    + "TestProject";
        }

        public string CreateMockFeed()
        {
            var feedPath = Path.GetDirectoryName(CurrentDirectory)
                + Path.DirectorySeparatorChar
                    + "TestFeed";

            new MockNugetFeedCreator(OriginalDirectory, CurrentDirectory, feedPath).Create();

            MockFeedPath = feedPath;

            return feedPath;
        }
    }
}

