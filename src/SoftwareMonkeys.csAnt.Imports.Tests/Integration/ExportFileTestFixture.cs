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
            // TODO: Better organize this test
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
			
			Console.WriteLine ("Relocating to source project...");
			Console.WriteLine ("");

			// Move to second project
			script.Relocate(sourceProjectDir);
			
			Console.WriteLine ("Initializing git repository...");
			Console.WriteLine ("");

			// Initialize git
			script.Git.Init();

			var sourceScriptsDir = sourceProjectDir
				+ Path.DirectorySeparatorChar
					+ "scripts";

			var sourceScriptFile = sourceScriptsDir
				+ Path.DirectorySeparatorChar
					+ "TestScript.cs";

			var sourceScriptContent = "test";
			
			Console.WriteLine ("Creating a script file...");
			Console.WriteLine ("");
            
            script.EnsureDirectoryExists(sourceScriptsDir);

			// Create the script file
			File.WriteAllText(sourceScriptFile, sourceScriptContent);

			Console.WriteLine ("Adding the dummy script file to git...");
			Console.WriteLine ("");

			// Add the script file to git
			script.Git.Add (sourceScriptFile);
			
			Console.WriteLine ("Committing file to git...");
			Console.WriteLine ("");

			// Git commit
			script.Git.Commit();
			
			Console.WriteLine ("Relocating to destination project...");
			Console.WriteLine ("");

			// Switch back to project one
			script.Relocate(destinationProjectDir);

            // Create the required file nodes
            script.CreateNodes();
			
			Console.WriteLine ("Adding source project as an import project...");
			Console.WriteLine ("");

			// Add project 2 as import
			script.Importer.AddImport("SourceProject", sourceProjectDir);

			Console.WriteLine ("Importing test script to destination project form source project...");
			Console.WriteLine ("");

			script.Importer.ImportFile("SourceProject", "scripts/TestScript.cs", "scripts", false);

			var expectedFile = destinationProjectDir
				+ Path.DirectorySeparatorChar
				+ "scripts"
				+ Path.DirectorySeparatorChar
					+ "TestScript.cs";

			
			Console.WriteLine ("Checking that the script was imported...");
			Console.WriteLine ("");

			Assert.IsTrue(File.Exists(expectedFile), "File wasn't copied from imports directory.");


			// Add another file in destination project
			var script2File = destinationProjectDir
				+ Path.DirectorySeparatorChar
				+ "scripts"
					+ Path.DirectorySeparatorChar
					+ "TestScript2.cs";

			var script2Content = "Test content 2";

			Console.WriteLine ("Creating a second test script in destination project...");
			Console.WriteLine ("");

			File.WriteAllText(script2File, script2Content);
			
			Console.WriteLine ("Exporting the second test script back to source project...");
			Console.WriteLine ("");

			script.Importer.ExportFile("SourceProject", script2File);
			
			var expectedFile2 = destinationProjectDir.TrimEnd(Path.DirectorySeparatorChar)
                + "-Imports"
                + Path.DirectorySeparatorChar
                + "SourceProject"
                + Path.DirectorySeparatorChar
                + "scripts"
				+ Path.DirectorySeparatorChar
				+ "TestScript2.cs";

            Console.WriteLine("Expected file after export:");
            Console.WriteLine(expectedFile2);

			Assert.IsTrue(File.Exists(expectedFile2), "New file wasn't copied back to imports directory.");

			// TODO: Check if needed
			/*Console.WriteLine ("Relocating to project two...");
			Console.WriteLine ("");

			// Move into project 2
			script.Relocate(project2Dir);
			
			Console.WriteLine ("Pulling git changes...");
			Console.WriteLine ("");
*/
			var expectedFile3Path = sourceProjectDir
				+ Path.DirectorySeparatorChar
					+ "scripts"
					+ Path.DirectorySeparatorChar
					+ Path.GetFileName(script2File);

			Assert.IsTrue(File.Exists(expectedFile3Path), "File wasn't pulled to destination project.");
		}
	}
}

