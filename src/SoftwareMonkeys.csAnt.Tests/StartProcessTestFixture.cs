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

            // TODO: Clean up
			script.FilesGrabber.GrabOriginalFiles(
				//"lib/cs-script/**",
				"lib/NUnit/**"//,
				//"scripts/HelloWorld.cs"
			);

			/*var csscriptExe = script.CurrentDirectory
				+ Path.DirectorySeparatorChar
				+ "lib"
				+ Path.DirectorySeparatorChar
				+ "cs-script"
				+ Path.DirectorySeparatorChar
				+ "cscs.exe";

			var helloWorldScript = script.CurrentDirectory
				+ Path.DirectorySeparatorChar
				+ "scripts"
				+ Path.DirectorySeparatorChar
					+ "HelloWorld.cs";

			script.StartProcess("mono", csscriptExe, helloWorldScript);
*/
            script.StartProcess ("echo", "Hello world!");
            
            //script.StartProcess ("mono", "lib/NUnit/bin/nunit-console.exe");

			Assert.IsTrue(script.Console.Output.Contains ("Hello world!"), "The output is incorrect.");
		}
	}
}

