using System;
using System.IO;
using NUnit.Framework;
using SoftwareMonkeys.csAnt.External.Nuget;
using SoftwareMonkeys.csAnt.External.Nuget.Tests.Mock;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.Processes;
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.csAnt.SetUp.Repack;
using SoftwareMonkeys.csAnt.Tests;
using SoftwareMonkeys.csAnt.Tests.Helpers;
using SoftwareMonkeys.csAnt.Versions;

namespace SoftwareMonkeys.csAnt.SetUpFromWebConsole.Tests.Integration
{
    [TestFixture]
    public class SetUpConsoleSpecifyStatusTestFixture : BaseTestFixture
    {
        public string LocalNugetFilePath { get;set; }

        public MockNugetFeedCreator FeedCreator { get; set; }

        public SetUpConsoleSpecifyStatusTestFixture ()
        {
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

            Console.WriteLine ("Identifying the git branch...");
            var branch = "branch";

            // Launch the setup file with a local nuget
            Console.WriteLine ("Starting the setup...");
            var processStarter = new DotNetProcessStarter();
            processStarter.Start(
                setupFileName,
                "-nuget=" + LocalNugetFilePath,
                "-source=" + FeedCreator.FeedPath,
                //"-source=" + Path.Combine(sourceDir, "pkg") + ";" + Path.Combine(sourceDir, "lib"), // TODO: Remove if not needed
                "-status=beta",
                "-branch=" + branch
                );

            Assert.IsFalse(processStarter.IsError, "An error occurred.");
        }
        

        public string PrepareSpecifyStatus(string setupFileName, string buildMode)
        {
            Console.WriteLine ("Preparing a setup test that involves specifying the status...");

            // Grab all the original files
            Console.WriteLine ("Grabbing original files...");
            var grabber = new FilesGrabber (
                OriginalDirectory,
                WorkingDirectory
                );

            Console.WriteLine ("Grabbing original files...");
            grabber.GrabOriginalFiles();

            Console.WriteLine ("Grabbing git files...");
            grabber.GrabGitFiles ();

            Console.WriteLine ("Initializing nodes...");
            var nodeManager = new ProjectNodeManager(WorkingDirectory);
            nodeManager.IncludeChildNodes = true;
            nodeManager.EnsureNodes();

            var currentNode = nodeManager.State.CurrentNode;

            // Set the status to beta
            Console.WriteLine ("Setting status and version in the file node...");
            currentNode.Properties["Status"] = "beta";
            currentNode.Properties["Version"] = "1.0.0.0";
            currentNode.Properties["Branch"] = "branch";
            currentNode.Save();

            // Build the solution
            new SolutionBuilder(buildMode).BuildSolution("csAnt.Tests");
            Console.WriteLine ("Building the solution...");

            // Repack the csAnt-SetUp.exe file to include dependencies
            Console.WriteLine ("Repacking setup file...");
            new SetUpRepacker(buildMode).Repack();

            Console.WriteLine ("Copying setup files to root...");
            new FileCopier(
                Path.Combine(WorkingDirectory, "bin/" + BuildMode.Value + "/packed"),
                WorkingDirectory
                ).Copy("*");

            // Create the package
            Console.WriteLine ("Packing the nuget package...");
            new NugetPacker(){
                Status=currentNode.Properties["Status"],
                Version=new Version(currentNode.Properties["Version"]),
                Branch=currentNode.Properties["Branch"]
            }.Pack(CurrentDirectory, "csAnt");

            Console.WriteLine ("Incrementing the version...");
            new VersionManager().IncrementVersion(currentNode, 2);

            // Reset the status back to alpha
            Console.WriteLine ("Resetting the status back to alpha...");
            currentNode.Properties["Status"] = "alpha";
            currentNode.Save();

            // Repackage
            Console.WriteLine ("Repackaging the nuget package...");
            new NugetPacker(){
                Status=currentNode.Properties["Status"],
                Version=new Version(currentNode.Properties["Version"])
            }.Pack(CurrentDirectory, "csAnt");

            // Create a test project directory
            Console.WriteLine ("Creating the test project path...");
            var testProjectDirectory = new TestInstallationCreator(OriginalDirectory, WorkingDirectory).CreateTestInstallationPath();

            var workingBinDirectory = WorkingDirectory
                + Path.DirectorySeparatorChar
                    + "bin"
                    + Path.DirectorySeparatorChar
                    + buildMode
                    + Path.DirectorySeparatorChar
                    + "packed";

            DirectoryChecker.EnsureDirectoryExists(testProjectDirectory);

            // Copy setup file to test project directory, ready to run
            Console.WriteLine ("Copying the setup file to the test project directory...");
            new FilesGrabber(
                workingBinDirectory,
                testProjectDirectory
                ).GrabOriginalFiles(
                setupFileName
                );

            // Create a mock nuget feed
            Console.WriteLine ("Creating the mock nuget feed...");
            FeedCreator = new MockNugetFeedCreator (OriginalDirectory, WorkingDirectory);
            FeedCreator.Create();

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

