using System;
using NUnit.Framework;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt.Tests.Scripting
{
    [TestFixture]
    public class LifecycleTestFixture : BaseScriptingTestFixture
    {
        [Test]
        public void Test_DoesSetUp ()
        {
            // Get a test script
            var script = GetTestScript ("TestScript");

            new FilesGrabber(
                script.OriginalDirectory,
                script.CurrentDirectory
                ).GrabOriginalScriptingFiles();

            // Create a sub script
            var subScript = script.ScriptExecutor.Activator.ActivateScript("HelloWorld");

            // Start the sub script (to ensure it's set up)
            subScript.Start();

            // Check that the parent script output contains the tearing down console output
            Assert.IsTrue (script.ConsoleWriter.Output.Contains("Setting up 'HelloWorld' script..."));
        }

        [Test]
        public void Test_DoesTearDown ()
        {
            // Get a test script
            var script = GetTestScript ("TestScript");
            
            new FilesGrabber(
                script.OriginalDirectory,
                script.CurrentDirectory
                ).GrabOriginalScriptingFiles();

            // Create a sub script
            using (var subScript = script.ScriptExecutor.Activator.ActivateScript("HelloWorld")) {
                // Start the sub script
                subScript.Start();
            }// Let the sub script dispose, and be torn down

            // Check that the parent script output contains the tearing down console output
            Assert.IsTrue (script.ConsoleWriter.Output.Contains("Tearing down 'HelloWorld' script..."));
        }
    }
}

