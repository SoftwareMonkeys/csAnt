using System;
using NUnit.Framework;
using System.IO;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.Tests;

namespace SoftwareMonkeys.csAnt.Imports.Tests
{
	[TestFixture]
	public class ImportFileTestFixture : BaseImportsIntegrationTestFixture
	{
		[Test]
		public void Test_ImportFile()
		{
            // TODO: Overhaul test to use Importer instead of using a dummy script

			var script = (DummyScript)GetDummyScript();

            new FilesGrabber(
                script.OriginalDirectory,
                script.CurrentDirectory
                ).GrabOriginalFiles();

			script.IsVerbose = true;

			var dir = Path.GetDirectoryName(script.CurrentDirectory);

            var projectsDir = dir;

			var destinationProjectDir = projectsDir
				+ Path.DirectorySeparatorChar
					+ "DestinationProject";

			var sourceProjectDir = projectsDir
				+ Path.DirectorySeparatorChar
					+ "SourceProject";
			
			Console.WriteLine ("");
			Console.WriteLine ("Destination project:");
			Console.WriteLine (destinationProjectDir);
			Console.WriteLine ("Source project:");
			Console.WriteLine (sourceProjectDir);
			Console.WriteLine ("");

			script.EnsureDirectoryExists(destinationProjectDir);
			script.EnsureDirectoryExists(sourceProjectDir);

			// Move to source project
			script.Relocate(sourceProjectDir);

            // Create files nodes
            script.CreateNodes();

			// Initialize git
			script.Git.Init();

			var scriptsDir = sourceProjectDir
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

			// Switch back to destination project
			script.Relocate(destinationProjectDir);

            // Create the required file nodes
            script.CreateNodes ();

			// Add project 2 as import
			script.Importer.AddImport("SourceProject", sourceProjectDir);

            // Import the file
			script.Importer.ImportFile("SourceProject", "scripts/TestScript.cs", "scripts", false);

			var expectedFile = destinationProjectDir
				+ Path.DirectorySeparatorChar
				+ "scripts"
				+ Path.DirectorySeparatorChar
					+ "TestScript.cs";

			Assert.IsTrue(File.Exists(expectedFile), "File wasn't copied.");
        }

        [Test]
        public void Test_ImportFile_FileExists()
        {
            // TODO: Overhaul test to use Importer instead of using a dummy script

            var script = (DummyScript)GetDummyScript();

            new FilesGrabber(
                script.OriginalDirectory,
                script.CurrentDirectory
            ).GrabOriginalFiles();

            script.IsVerbose = true;

            var dir = Path.GetDirectoryName(script.CurrentDirectory);

            var projectsDir = dir;

            var destinationProjectDir = projectsDir
                + Path.DirectorySeparatorChar
                + "DestinationProject";

            var sourceProjectDir = projectsDir
                + Path.DirectorySeparatorChar
                + "SourceProject";

            Console.WriteLine ("");
            Console.WriteLine ("Destination project:");
            Console.WriteLine (destinationProjectDir);
            Console.WriteLine ("Source project:");
            Console.WriteLine (sourceProjectDir);
            Console.WriteLine ("");

            script.EnsureDirectoryExists(destinationProjectDir);
            script.EnsureDirectoryExists(sourceProjectDir);

            // Move to source project
            script.Relocate(sourceProjectDir);

            // Create files nodes
            script.CreateNodes();

            // Initialize git
            script.Git.Init();

            var scriptsDir = sourceProjectDir
                + Path.DirectorySeparatorChar
                + "scripts";

            var scriptFile = scriptsDir
                + Path.DirectorySeparatorChar
                + "TestScript.cs";

            script.EnsureDirectoryExists(scriptsDir);

            var scriptContent = "after";

            // Create the script file
            File.WriteAllText(scriptFile, scriptContent);

            // Add the script file to git
            script.Git.Add (scriptFile);

            // Git commit
            script.Git.Commit();

            // Switch back to destination project
            script.Relocate(destinationProjectDir);

            // Create the required file nodes
            script.CreateNodes ();

            // Add project 2 as import
            script.Importer.AddImport("SourceProject", sourceProjectDir);

            var destinationScriptFile = destinationProjectDir
                + Path.DirectorySeparatorChar
                + "scripts"
                + Path.DirectorySeparatorChar
                + "TestScript.cs";

            DirectoryChecker.EnsureDirectoryExists(Path.GetDirectoryName(destinationScriptFile));

            File.WriteAllText(destinationScriptFile, "before");

            // Import the file
            script.Importer.ImportFile("SourceProject", "scripts/TestScript.cs", "scripts", false);

            Assert.AreEqual("after", File.ReadAllText(destinationScriptFile), "File wasn't imported.");

            var bakDir = PathConverter.ToAbsolute("_bak");

            // Check that the file was backed up
            Assert.IsTrue(Directory.Exists(bakDir));
        }
	}
}

