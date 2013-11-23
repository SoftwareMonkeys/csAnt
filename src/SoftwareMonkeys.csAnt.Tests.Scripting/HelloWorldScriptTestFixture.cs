using System;
using NUnit.Framework;

namespace SoftwareMonkeys.csAnt.Tests
{
	[TestFixture]
	public class HelloWorldTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_TestHelloWorldScript()
		{
			var script = GetDummyScript();

			script.FilesGrabber.GrabOriginalScriptingFiles();

			script.ExecuteScript("Test_HelloWorld");

			Assert.IsFalse(script.IsError);
		}
        
        [Test]
        public void Test_HelloWorldScript()
        {
            var script = GetDummyScript();

            script.FilesGrabber.GrabOriginalScriptingFiles();

            script.ExecuteScript("HelloWorld");

            Assert.IsFalse(script.IsError);
        }
	}
}

