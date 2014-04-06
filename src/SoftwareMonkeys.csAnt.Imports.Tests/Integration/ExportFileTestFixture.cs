using System;
using NUnit.Framework;
using System.IO;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt.Imports.Tests
{
	[TestFixture]
	public class ExportFileTestFixture : BaseImportsIntegrationTestFixture
	{
		[Test]
		public void Test_ExportFile()
		{
            // TODO: Overhaul test to use Importer instead of using a dummy script

			var script = (BaseScript)GetDummyScript();

            new FilesGrabber(
                script.OriginalDirectory,
                script.CurrentDirectory
                ).GrabOriginalFiles();

			script.IsVerbose = true;

			var dir = Path.GetDirectoryName(script.CurrentDirectory);

			var projectsDir = dir
				+ Path.DirectorySeparatorChar
					+ "Projects";

			var sourceProjectDir = projectsDir
				+ Path.DirectorySeparatorChar
					+ "SourceProject";

			var destinationProjectDir = projectsDir
				+ Path.DirectorySeparatorChar
					+ "DestinationProject";
			
			Console.WriteLine ("");
			Console.WriteLine ("Source project:");
			Console.WriteLine (sourceProjectDir);
			Console.WriteLine ("Destination project:");
			Console.WriteLine (destinationProjectDir);
			Console.WriteLine ("");

			Console.WriteLine ("Creating test project directories...");
			Console.WriteLine ("");

			script.EnsureDirectoryExists(sourceProjectDir);
			script.EnsureDirectoryExists(destinationProjectDir);
			
			Console.WriteLine ("Relocating to project 2...");
			Console.WriteLine ("");

			// Move to second project
			script.Relocate(destinationProjectDir);
			
			Console.WriteLine ("Initializing git repository...");
			Console.WriteLine ("");

			// Initialize git
			script.Git.Init();

			var destinationScriptsDir = destinationProjectDir
				+ Path.DirectorySeparatorChar
					+ "scripts";

			var destinationScriptFile = destinationScriptsDir
				+ Path.DirectorySeparatorChar
					+ "TestScript.cs";

			script.EnsureDirectoryExists(destinationScriptsDir);

			var destinationScriptContent = "test";
			
			Console.WriteLine ("Creating a dummy script file...");
			Console.WriteLine ("");

			// Create the script file
			File.WriteAllText(destinationScriptFile, destinationScriptContent);

			Console.WriteLine ("Adding the dummy script file to git...");
			Console.WriteLine ("");

			// Add the script file to git
			script.Git.Add (destinationScriptFile);
			
			Console.WriteLine ("Committing file to git...");
			Console.WriteLine ("");

			// Git commit
			script.Git.Commit();
			
			Console.WriteLine ("Relocating to project 1...");
			Console.WriteLine ("");

			// Switch back to project one
			script.Relocate(sourceProjectDir);

            // Create the required file nodes
            script.CreateNodes();
			
			Console.WriteLine ("Adding project 2 as an import project...");
			Console.WriteLine ("");

			// Add project 2 as import
			script.Importer.AddImport("DestinationProject", destinationProjectDir);

			Console.WriteLine ("Importing test script to project from project two...");
			Console.WriteLine ("");

			script.Importer.ImportFile("DestinationProject", "scripts/TestScript.cs", "scripts", false);

			var expectedFile = sourceProjectDir
				+ Path.DirectorySeparatorChar
				+ "scripts"
				+ Path.DirectorySeparatorChar
					+ "TestScript.cs";

			
			Console.WriteLine ("Checking that the script was imported...");
			Console.WriteLine ("");

			Assert.IsTrue(File.Exists(expectedFile), "File wasn't copied from imports directory.");


			// Add another file
			var script2File = sourceProjectDir
				+ Path.DirectorySeparatorChar
				+ "scripts"
					+ Path.DirectorySeparatorChar
					+ "TestScript2.cs";

			var script2Content = "Test content 2";

			Console.WriteLine ("Creating a second test script in project one...");
			Console.WriteLine ("");

			File.WriteAllText(script2File, script2Content);
			
			Console.WriteLine ("Exporting the second test script to project two...");
			Console.WriteLine ("");

			script.Importer.ExportFile("DestinationProject", script2File);
			
			var expectedFile2 = script.Importer.StagingDirectory
				+ Path.DirectorySeparatorChar
				+ "DestinationProject"
				+ Path.DirectorySeparatorChar
				+ "scripts"
				+ Path.DirectorySeparatorChar
				+ "TestScript2.cs";

			Assert.IsTrue(File.Exists(expectedFile2), "File wasn't copied to imports directory.");

			// TODO: Check if needed
			/*Console.WriteLine ("Relocating to project two...");
			Console.WriteLine ("");

			// Move into project 2
			script.Relocate(project2Dir);
			
			Console.WriteLine ("Pulling git changes...");
			Console.WriteLine ("");
*/
			var expectedFile3Path = destinationProjectDir
				+ Path.DirectorySeparatorChar
					+ "scripts"
					+ Path.DirectorySeparatorChar
					+ Path.GetFileName(script2File);

			Assert.IsTrue(File.Exists(expectedFile3Path), "File wasn't pulled to project two.");
		}
	}
}

