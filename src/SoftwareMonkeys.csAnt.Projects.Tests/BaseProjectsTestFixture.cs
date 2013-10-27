using System;
using SoftwareMonkeys.csAnt.Tests;
using System.IO;
using System.Collections.Generic;

namespace SoftwareMonkeys.csAnt.Projects.Tests
{
	public class BaseProjectsTestFixture : BaseTestFixture
	{
		public BaseProjectsTestFixture ()
		{
		}

		public new TestProjectScript GetTestScript()
		{
			var testScript = new TestProjectScript();

			InitializeTestScript(testScript);

			//testScript.IsVerbose = true;

			//var actualProjectDir = ProjectDirectory;

			//testScript.ProjectDirectory = testScript.GetTmpDir();

			// TODO: Check if this should be removed. Copying of test files should probably be explicitly called by any test that requires it
			//testScript.CopyTestFiles(actualProjectDir, testScript.ProjectDirectory);

			return testScript;
		}

		public void GrabOriginalScripts (
			ITestScript script,
			params string[] scriptNames
		)
		{
			List<string> list = new List<string> ();

			foreach (var name in scriptNames) {
				list.Add ("/scripts/" + name + ".cs");
				list.Add ("/scripts/" + name + "/**");
			}

			GrabOriginalFiles(
				script,
				list.ToArray()
			);
		}

		public void GrabOriginalScriptingFiles (ITestScript script)
		{
			GrabOriginalFiles(
				script,
				"/*.node",
				"/*.sh",
				"/lib/**.dll",
				"/src/**.cs",
				"/src/**.csproj",
				"/src/**.sln",
				"/scripts/**"
			);
		}

		public void GrabOriginalFiles (ITestScript script, params string[] patterns)
		{
			Console.WriteLine ("");
			Console.WriteLine ("Grabbing original project files...");
			Console.WriteLine ("From:");
			Console.WriteLine (script.OriginalDirectory);
			Console.WriteLine ("To:");
			Console.WriteLine (script.CurrentDirectory);
			Console.WriteLine ("");

			foreach (var file in script.FindFiles (script.OriginalDirectory, patterns)) {
				Console.WriteLine (file.Replace(script.OriginalDirectory, ""));

				var toFile = file.Replace(script.OriginalDirectory, script.CurrentDirectory);

				if (!Directory.Exists(Path.GetDirectoryName(toFile)))
					Directory.CreateDirectory(Path.GetDirectoryName(toFile));

				File.Copy (file, toFile);
			}
		}
	}
}

