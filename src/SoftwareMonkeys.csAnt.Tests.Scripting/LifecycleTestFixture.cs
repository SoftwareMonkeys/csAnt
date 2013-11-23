using System;
using NUnit.Framework;

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

            script.FilesGrabber.GrabOriginalScriptingFiles();

            // Create a sub script
            var subScript = script.ActivateScript("HelloWorld");

            // Start the sub script (to ensure it's set up)
            subScript.Start();

            // Check that the parent script output contains the tearing down console output
            Assert.IsTrue (script.Console.Output.Contains("Setting up 'HelloWorld' script..."));
        }

        [Test]
        public void Test_DoesTearDown ()
        {
            // Get a test script
            var script = GetTestScript ("TestScript");
            
            script.FilesGrabber.GrabOriginalScriptingFiles();

            // Create a sub script
            using (var subScript = script.ActivateScript("HelloWorld")) {
                // Start the sub script
                subScript.Start();
            }// Let the sub script dispose, and be torn down

            // Check that the parent script output contains the tearing down console output
            Assert.IsTrue (script.Console.Output.Contains("Tearing down 'HelloWorld' script..."));
        }
    }
}

