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
			var script = GetTestScript();

			GrabOriginalFiles(
				script,
				"lib/cs-script/**",
				"lib/csAnt/**",
				"scripts/HelloWorld.cs"
			);

			var csscriptExe = script.CurrentDirectory
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

			Assert.IsTrue(script.Console.Output.Contains ("Hello world!"));
		}
	}
}

