using System;
using NUnit.Framework;
using System.IO;

namespace SoftwareMonkeys.csAnt.Tests.Scripts
{
    [TestFixture]
    public class ExecuteScriptTestFixture : BaseScriptingTestFixture
    {
        // TODO: Rename this test fixture to something more appropriate as ExecuteScript isn't being used now
        [Test]
        public void Test_Start_CheckHtmlTestResults()
        {
            /*/var f = new HelloWorldTestFixture();
            f.SetUp();
            f.Test_HelloWorldScript();
            f.TearDown();*/
            var script = GetDummyScript();

            script.FilesGrabber.GrabOriginalScriptingFiles();

            var helloWorldScript = script.ActivateScript<ITestScript>("Test_HelloWorld");

            helloWorldScript.Start();

            Assert.IsFalse(helloWorldScript.IsError);

            var htmlResultFileNamer = new ScriptHtmlResultFileNamer();

            var htmlResultsDir = htmlResultFileNamer.GetHtmlResultsDirectory(helloWorldScript);

            Console.WriteLine(htmlResultsDir);

            Assert.AreEqual(2, Directory.GetFiles(htmlResultsDir).Length);

            var subDir = Directory.GetDirectories(htmlResultsDir)[0];

            Assert.AreEqual(2, Directory.GetFiles(DirechtmlResultsDir).Length);
        }
    }
}

