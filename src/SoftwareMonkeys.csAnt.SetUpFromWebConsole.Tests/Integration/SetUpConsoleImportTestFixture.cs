using System;
using SoftwareMonkeys.csAnt.SetUp.Tests;
using SoftwareMonkeys.csAnt.SourceControl.Git;
using NUnit.Framework;
using SoftwareMonkeys.csAnt.Processes;
using SoftwareMonkeys.csAnt.Tests;

namespace SoftwareMonkeys.csAnt.SetUpFromWebConsole.Tests.Integration
{
    [TestFixture]
    public class SetUpConsoleImportTestFixture : BaseTestFixture
    {
        public SetUpTestPreparer Preparer { get; set; }

        public SetUpConsoleImportTestFixture ()
        {
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

            // Prepare the test and place the setup file in the test installation directory
            var installationDirectory = PrepareImport(setupFileName, buildMode);

            // Set the import path to the current working directory
            var importPath = WorkingDirectory;

            // Move to the test project directory
            Environment.CurrentDirectory = installationDirectory;

            // Launch the setup file with a local nuget
            var processStarter = new DotNetProcessStarter();
            processStarter.Start(
                setupFileName,
                "-nuget=" + Preparer.LocalNugetFilePath,
                "-source=" + Preparer.FeedCreator.FeedPath,
                "-import=" + importPath
                );

            Assert.IsFalse(processStarter.IsError, "An error occurred.");
        }

        public string PrepareImport(string setupFileName, string buildMode)
        {
            new Gitter().Clone(OriginalDirectory, CurrentDirectory);

            Preparer = new SetUpTestPreparer (OriginalDirectory, WorkingDirectory);

            return Preparer.Prepare(setupFileName, buildMode);
        }
    }
}

