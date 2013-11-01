using System;
using System.IO;
using NUnit.Framework;

namespace SoftwareMonkeys.csAnt.Projects.Tests
{
	[TestFixture]
	public class ImportSyncTestFixture : BaseProjectsTestFixture
	{
		[Test]
		public void Test_ImportSync()
		{
			var testScript = GetTestScript();

			Console.WriteLine ("Project dir: " + testScript.ProjectDirectory);

			// Sync the files
			testScript.ImportSync(
				"csAnt",
				GetOriginalDirectory(testScript) // Use the actual project directory from the test fixture, not the temporary directory from the test script
			);

			var tmpFile = testScript.ProjectDirectory
				+ Path.DirectorySeparatorChar
				+ "TestFile.txt";

			File.WriteAllText(tmpFile, "Test content");

			// Sync the files again
			testScript.ImportSync(
				"csAnt",
				GetOriginalDirectory(testScript) // Use the actual project directory from the test fixture, not the temporary directory from the test script
			);

		}
	}
}

