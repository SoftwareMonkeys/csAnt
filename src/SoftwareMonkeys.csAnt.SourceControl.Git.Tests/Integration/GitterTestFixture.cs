using System;
using NUnit.Framework;
using System.IO;

namespace SoftwareMonkeys.csAnt.SourceControl.Git.Tests
{
	[TestFixture]
	public class GitterTestFixture : BaseGitIntegrationTestFixture
	{
		[Test]
		public void Test_Init_Add_Commit_Clone()
		{
			// Create a test script
			var testScript = GetDummyScript("TestScript");

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
			testScript.Git.Init(tmpProjectDir);

			// Create a test file path
			var testFile = tmpProjectDir
				+ Path.DirectorySeparatorChar
				+ "TestFile.txt";

			var testContents = "Test contents";

			// Create the test file
			File.WriteAllText(testFile, testContents);

			Console.WriteLine ("Adding test file...");

			// Add the test file
			testScript.Git.Add(testFile);

			Console.WriteLine ("Committing...");

			// Commit the test file
			testScript.Git.Commit();

			Console.WriteLine("Cloning...");

			// Clone the temporary project into a new directory
			testScript.Git.Clone (tmpProjectDir, tmpProjectCloneDir);

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

