using System;
using NUnit.Framework;
using SoftwareMonkeys.csAnt.SetUp;
using SoftwareMonkeys.csAnt.Tests.Helpers;


namespace SoftwareMonkeys.csAnt.SetUpFromLocalConsole.Tests.Unit
{
    [TestFixture]
    public class SetUpFromLocalConsoleUnitTestFixture : BaseSetUpFromLocalConsoleUnitTestFixture
    {
        [Test]
        public void Test_SetUpFromLocalConsole()
        {
            Prepare();

            new SetUpFromLocalConsoleRetriever().Retrieve(OriginalDirectory, WorkingDirectory);

            new SetUpFromLocalConsoleLauncher().Launch(OriginalDirectory, WorkingDirectory);

            new HelloWorldScriptLauncher().Launch();
        }

        public void Prepare()
        {
            var dir = WorkingDirectory;

            // TODO: Avoid creating a script by using the Relocator component and a dedicated ScriptExecutor component
            var script = GetDummyScript();

            script.Relocate(OriginalDirectory);

            // TODO: See if there's a faster way to prepare
            script.ExecuteScript("Repack", "-mode:" + BuildMode.Value);
            script.ExecuteScript("CopyBinToRoot", "-mode:" + BuildMode.Value);

            script.Relocate(dir);
        }
    }
}

