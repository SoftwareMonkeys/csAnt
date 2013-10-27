using System;
using System.IO;
using NUnit.Framework;

namespace SoftwareMonkeys.csAnt.Projects.Tests
{
	[TestFixture]
	public class GitSyncTestFixture : BaseProjectsTestFixture
	{
		[Test]
		public void Test_GitSync()
		{
			var testScript = GetTestScript();

			Console.WriteLine ("Project dir: " + testScript.ProjectDirectory);

			// Sync the files
			testScript.GitSync(
				"csAnt",
				GetOriginalDirectory(testScript) // Use the actual project directory from the test fixture, not the temporary directory from the test script
			);

			var tmpFile = testScript.ProjectDirectory
				+ Path.DirectorySeparatorChar
				+ "TestFile.txt";

			File.WriteAllText(tmpFile, "Test content");

			// Sync the files again
			testScript.GitSync(
				"csAnt",
				GetOriginalDirectory(testScript) // Use the actual project directory from the test fixture, not the temporary directory from the test script
			);

		}
	}
}

