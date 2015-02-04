using System;
using NUnit.Framework;
using SoftwareMonkeys.csAnt.Processes;
using SoftwareMonkeys.csAnt.SourceControl.Git;
using SoftwareMonkeys.csAnt.SetUp.Tests;
using SoftwareMonkeys.csAnt.External.Nuget.Tests.Mock;
using SoftwareMonkeys.csAnt.Tests;

namespace SoftwareMonkeys.csAnt.SetUpFromWebConsole.Tests.Integration
{
    [TestFixture]
    public class SetUpConsoleCloneTestFixture : BaseTestFixture
    {
        public string LocalNugetFilePath { get;set; }

        public MockNugetFeedCreator FeedCreator { get; set; }

        public SetUpConsoleCloneTestFixture ()
        {
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
                "-source=" + FeedCreator.FeedPath,
                "-clone=" + clonePath
                );

            Assert.IsFalse(processStarter.IsError, "An error occurred.");
        }
        

        public string PrepareClone(string setupFileName, string buildMode)
        {
            new Gitter().Clone(OriginalDirectory, CurrentDirectory);

            var preparer = new SetUpTestPreparer (OriginalDirectory, WorkingDirectory);

            preparer.Prepare(setupFileName, buildMode);

            FeedCreator = preparer.FeedCreator;

            return preparer.TestInstallation.InstallationPath;
        }
    }
}

