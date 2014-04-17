using System;
using NUnit.Framework;
using SoftwareMonkeys.csAnt.SetUp;
using SoftwareMonkeys.csAnt.Tests.Helpers;
using SoftwareMonkeys.csAnt.IO;
using System.IO;
using SoftwareMonkeys.csAnt.SetUp.Deploy.Retrieve;
using SoftwareMonkeys.csAnt.SetUp.Deploy.Launch;

namespace SoftwareMonkeys.csAnt.SetUpFromLocalConsole.Tests.Integration
{
    // TODO: Make this an integration test?
    [TestFixture]
    public class SetUpFromLocalConsoleIntegrationTestFixture : BaseSetUpFromLocalConsoleIntegrationTestFixture
    {
        [Test]
        public void Test_SetUpFromLocalConsole()
        {
            Prepare();

            var testProjectDir = Path.GetDirectoryName(CurrentDirectory)
                + Path.DirectorySeparatorChar
                    + "TestProject";

            var fromDir = CurrentDirectory;

            new SetUpFromLocalConsoleRetriever().Retrieve(fromDir, testProjectDir);

            // Launch the setup from local console
            new SetUpFromLocalConsoleLauncher().Launch(fromDir, testProjectDir);

            new HelloWorldScriptLauncher().Launch();
        }

        public void Prepare()
        {
            new FilesGrabber(
                OriginalDirectory,
                WorkingDirectory
                ).GrabOriginalFiles();

            // TODO: Avoid creating a script by using the dedicated ScriptExecutor component
            var script = GetDummyScript();

            // TODO: See if there's a faster way to prepare
            script.ExecuteScript("EnsureBuild", "-mode:" + BuildMode.Value);
            script.ExecuteScript("Repack", "-mode:" + BuildMode.Value);
            script.ExecuteScript("CopyBinToRoot", "-mode:" + BuildMode.Value);


        }
    }
}

