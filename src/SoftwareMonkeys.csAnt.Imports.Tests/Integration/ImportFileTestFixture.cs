using System;
using NUnit.Framework;
using System.IO;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt.Tests
{
	[TestFixture]
	public class ImportFileTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_ImportFile()
		{
			var script = (DummyScript)GetDummyScript();

            new FilesGrabber(
                script.OriginalDirectory,
                script.CurrentDirectory
                ).GrabOriginalFiles();

			script.IsVerbose = true;

			var dir = Path.GetDirectoryName(script.CurrentDirectory);

            var projectsDir = dir;

			var project1Dir = projectsDir
				+ Path.DirectorySeparatorChar
					+ "ProjectOne";

			var project2Dir = projectsDir
				+ Path.DirectorySeparatorChar
					+ "ProjectTwo";
			
			Console.WriteLine ("");
			Console.WriteLine ("Project One:");
			Console.WriteLine (project1Dir);
			Console.WriteLine ("Project Two:");
			Console.WriteLine (project2Dir);
			Console.WriteLine ("");

			script.EnsureDirectoryExists(project1Dir);
			script.EnsureDirectoryExists(project2Dir);

			// Move to second project
			script.Relocate(project2Dir);

            // Create files nodes
            script.CreateNodes();

			// Initialize git
			script.Git.Init();

			var scriptsDir = project2Dir
				+ Path.DirectorySeparatorChar
					+ "scripts";

			var scriptFile = scriptsDir
				+ Path.DirectorySeparatorChar
					+ "TestScript.cs";

			script.EnsureDirectoryExists(scriptsDir);

			var scriptContent = "test";

			// Create the script file
			File.WriteAllText(scriptFile, scriptContent);

			// Add the script file to git
			script.Git.Add (scriptFile);

			// Git commit
			script.Git.Commit();

			// Switch back to project one
			script.Relocate(project1Dir);

            // Create the required file nodes
            script.CreateNodes ();

			// Add project 2 as import
			script.Importer.AddImport("ProjectTwo", project2Dir);

            // Import the file
			script.Importer.ImportFile("ProjectTwo", "scripts/TestScript.cs", "scripts", false);

			var expectedFile = project1Dir
				+ Path.DirectorySeparatorChar
				+ "scripts"
				+ Path.DirectorySeparatorChar
					+ "TestScript.cs";

			Assert.IsTrue(File.Exists(expectedFile), "File wasn't copied.");
		}
	}
}

