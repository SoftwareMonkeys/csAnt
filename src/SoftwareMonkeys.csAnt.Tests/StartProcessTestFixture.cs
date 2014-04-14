using System;
using NUnit.Framework;
using System.IO;

namespace SoftwareMonkeys.csAnt.Tests
{
	[TestFixture]
	public class StartProcessTestFixture : BaseTestFixture
	{
        // TODO: Move this to Process.Tests library and rename to ProcessStarterTestFixture. Test the ProcessStarter component instead of the StartProcess function so it's a true unit test
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

