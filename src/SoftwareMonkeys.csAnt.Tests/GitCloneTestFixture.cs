using System;
using NUnit.Framework;
using System.IO;

namespace SoftwareMonkeys.csAnt.Tests
{
	[TestFixture]
	public class GitCloneTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_GitClone()
		{
			// Create a test script
			var testScript = new TestScript(this);

			// Create a temporary path
			var tmpDir = testScript.GetTmpDir();

			Console.WriteLine ("Tmp dir: " + tmpDir);

			// Create a temporary path to the source project
			var tmpProjectDir = tmpDir.TrimEnd(Path.DirectorySeparatorChar)
				+ Path.DirectorySeparatorChar
				+ "TestProject";

			// Create the source project directory
			Directory.CreateDirectory(tmpProjectDir);

			// Create a temporary path to the destination project
			var tmpProjectCloneDir = tmpDir.TrimEnd(Path.DirectorySeparatorChar)
				+ Path.DirectorySeparatorChar
				+ "TestProjectClone";

			// Create the destination project directory
			Directory.CreateDirectory(tmpProjectCloneDir);

			// Initialize source git repo
			testScript.GitInit(tmpProjectDir);

			// Create a test file path
			var testFile = tmpProjectDir
				+ Path.DirectorySeparatorChar
				+ "TestFile.txt";

			var testContents = "Test contents";

			// Create the test file
			File.WriteAllText(testFile, testContents);

			Console.WriteLine ("Adding test file...");

			// Add the test file
			testScript.GitAdd(testFile);

			Console.WriteLine ("Committing...");

			// Commit the test file
			testScript.GitCommit();

			Console.WriteLine("Cloning...");

			// Clone the temporary project into a new directory
			testScript.GitClone (tmpProjectDir, tmpProjectCloneDir);

			var clonedTestFile = tmpProjectCloneDir
				+ Path.DirectorySeparatorChar
				+ "TestFile.txt";

			// Assert that the file was clone
			Assert.IsTrue(File.Exists(clonedTestFile), "The test file wasn't cloned.");

			Assert.AreEqual(testContents, File.ReadAllText(clonedTestFile), "The cloned test file doesn't have the expected contents.");

			// Delete the entire temporary directory
			//Directory.Delete (tmpDir, true);
		}
	}
}

