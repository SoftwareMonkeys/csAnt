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



        [Test]
        public void Test_SetUp_Clone()
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
            var testProjectDirectory = PrepareClone(setupFileName, buildMode);

            // Set the clone path to the current working directory
            var clonePath = WorkingDirectory;

            // Move to the test project directory
            Environment.CurrentDirectory = testProjectDirectory;

            // Launch the setup file with a local nuget
            var processStarter = new DotNetProcessStarter();
            processStarter.Start(
                setupFileName,
                "-nuget=" + LocalNugetFilePath,
                "-source=" + MockFeedPath,
                "-clone=" + clonePath
            );

            Assert.IsFalse(processStarter.IsError);
        }


        [Test]
        public void Test_SetUp_SpecifyStatus()
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

            var sourceDir = CurrentDirectory;

            // Prepare the test and place the setup file in the test project directory
            var testProjectDirectory = PrepareSpecifyStatus(setupFileName, buildMode);

            // Set the import path to the current working directory
            var importPath = WorkingDirectory;

            // Move to the test project directory
            Environment.CurrentDirectory = testProjectDirectory;

            // Launch the setup file with a local nuget
            var processStarter = new DotNetProcessStarter();
            processStarter.Start(
                setupFileName,
                "-nuget=" + LocalNugetFilePath,
                "-source=" + Path.Combine(sourceDir, "pkg"),
                "-status=beta"
                );

            Assert.IsFalse(processStarter.IsError);
        }

        public string Prepare(string setupFileName, string buildMode)
        {
            // Grab all the original files
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


            return testProjectDirectory;
        }

        public string PrepareImport(string setupFileName, string buildMode)
        {
            new Gitter().Clone(OriginalDirectory, CurrentDirectory);

            return Prepare(setupFileName, buildMode);
        }

        public string PrepareClone(string setupFileName, string buildMode)
        {
            new Gitter().Clone(OriginalDirectory, CurrentDirectory);

            return Prepare(setupFileName, buildMode);
        }

        public string PrepareSpecifyStatus(string setupFileName, string buildMode)
        {
            // Grab all the original files
            new FilesGrabber(
                OriginalDirectory,
                WorkingDirectory
                ).GrabOriginalFiles();

            var nodeManager = new ProjectNodeManager(WorkingDirectory);
            nodeManager.IncludeChildNodes = true;
            nodeManager.EnsureNodes();

            var currentNode = nodeManager.State.CurrentNode;

            // Set the status to beta
            currentNode.Properties["Status"] = "beta";
            currentNode.Properties["Version"] = "1.0.0.0";
            currentNode.Save();

            // Build the solution
            new SolutionBuilder(buildMode).BuildSolution("csAnt.Tests");

            // Repack the csAnt-SetUp.exe file to include dependencies
            new SetUpRepacker(buildMode).Repack();

            new FileCopier(
                Path.Combine(WorkingDirectory, "bin/" + BuildMode.Value + "/packed"),
                WorkingDirectory
                ).Copy("*");

            // Create the package
            new NugetPacker(){
                Status=currentNode.Properties["Status"],
                Version=new Version(currentNode.Properties["Version"])
            }.Pack(CurrentDirectory, "csAnt");

            new VersionManager().IncrementVersion(currentNode, 2);

            // Reset the status back to alpha
            currentNode.Properties["Status"] = "alpha";
            currentNode.Save();

            // Repackage
            new NugetPacker(){
                Status=currentNode.Properties["Status"],
                Version=new Version(currentNode.Properties["Version"])
            }.Pack(CurrentDirectory, "csAnt");

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

            // Create the path to a local copy of nuget.exe (so it doesn't get downloaded from the web)
            LocalNugetFilePath = WorkingDirectory
                + Path.DirectorySeparatorChar
                    + "lib"
                    + Path.DirectorySeparatorChar
                    + "nuget.exe";

            return testProjectDirectory;
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

