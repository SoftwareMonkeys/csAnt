using System;
using NUnit.Framework;
using System.IO;

namespace SoftwareMonkeys.csAnt.Tests
{
	[TestFixture]
	public class StartProcessTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_StartProcess()
		{
			var script = GetDummyScript();

            script.StartProcess ("echo", "Hello world!");

			Assert.IsTrue(script.ConsoleWriter.Output.Contains ("\nHello world!\n"), "The output is incorrect.");
		}
        
        [Test]
        public void Test_StartProcess_Error()
        {
            var script = GetDummyScript();

            script.StartProcess ("missingcommand", "Hello world!");

            Assert.IsTrue(script.ConsoleWriter.Output.Contains ("Win32Exception: ApplicationName='missingcommand'"), "The output is incorrect.");
        }
	}
}

