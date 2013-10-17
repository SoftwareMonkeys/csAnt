using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Tests
{
	public class BaseTestFixture
	{
		public BaseTestFixture ()
		{
		}

		public string GetProjectRoot()
		{
			var path = Path.GetDirectoryName(
				Environment.CurrentDirectory
			);

			path = Path.GetDirectoryName(
				path
			);

			return path;
		}
		
		public TestScript GetTestScript()
		{
			var testScript = new TestScript(
				"TestScript",
				this
			);

			testScript.IsVerbose = true;

			var actualProjectDir = testScript.CurrentDirectory;

			var tmpDir = testScript.GetTmpDir();

			// TODO: Remove if not needed
			//testScript.CopyTestFiles(actualProjectDir, tmpDir);
			
			testScript.CurrentDirectory = tmpDir;
			
			Console.WriteLine ("Test directory: " + testScript.CurrentDirectory);
			Console.WriteLine ("Actual project dir: " + actualProjectDir);

			return testScript;
		}
	}
}

