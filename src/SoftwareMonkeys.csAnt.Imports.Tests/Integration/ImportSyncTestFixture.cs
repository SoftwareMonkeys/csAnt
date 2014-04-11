using System;
using System.IO;
using NUnit.Framework;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt.Imports.Tests
{
	[TestFixture]
	public class ImportSyncTestFixture : BaseImportsIntegrationTestFixture
	{
		[Test]
		public void Test_ImportSync()
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

			var sourceDir = projectsDir
				+ Path.DirectorySeparatorChar
					+ "SourceProject";

			var destinationDir = projectsDir
				+ Path.DirectorySeparatorChar
					+ "DestinationProject";
			
			Console.WriteLine ("");
			Console.WriteLine ("Source Project:");
			Console.WriteLine (sourceDir);
			Console.WriteLine ("Destination Project:");
			Console.WriteLine (destinationDir);
			Console.WriteLine ("");

			Console.WriteLine ("Creating test project directories...");
			Console.WriteLine ("");

			script.EnsureDirectoryExists(sourceDir);
			script.EnsureDirectoryExists(destinationDir);
			
			Console.WriteLine ("Relocating to destination project...");
			Console.WriteLine ("");

			// Move to second project
			script.Relocate(destinationDir);
            
            // Create the required file nodes
            script.CreateNodes();
			
			Console.WriteLine ("Initializing git repository...");
			Console.WriteLine ("");

			// Initialize git
			script.Git.Init();

			var scriptsDir = destinationDir
				+ Path.DirectorySeparatorChar
					+ "scripts";

			var scriptFile = scriptsDir
				+ Path.DirectorySeparatorChar
					+ "TestScript.cs";

			script.EnsureDirectoryExists(scriptsDir);

			var scriptContent = "test";
			
			Console.WriteLine ("Creating a dummy script file...");
			Console.WriteLine ("");

			// Create the script file
			File.WriteAllText(scriptFile, scriptContent);

			Console.WriteLine ("Adding the dummy script file to git...");
			Console.WriteLine ("");

			// Add the script file to git
			script.Git.Add (scriptFile);
			
			Console.WriteLine ("Committing file to git...");
			Console.WriteLine ("");

			// Git commit
			script.Git.Commit();
			
			Console.WriteLine ("Relocating to source project...");
			Console.WriteLine ("");

			// Switch back to project one
			script.Relocate(sourceDir);

            // Create the required file nodes
            script.CreateNodes();
			
			Console.WriteLine ("Adding destination project as an import project...");
			Console.WriteLine ("");

			// Add destination project as import
			script.Importer.AddImport("DestinationProject", destinationDir);

			Console.WriteLine ("Importing test script to project from project two...");
			Console.WriteLine ("");

			script.Importer.ImportFile("DestinationProject", "scripts/TestScript.cs", "scripts", false);

			var expectedFile = sourceDir
				+ Path.DirectorySeparatorChar
				+ "scripts"
				+ Path.DirectorySeparatorChar
					+ "TestScript.cs";

			
			Console.WriteLine ("Checking that the script was imported...");
			Console.WriteLine ("");

			Assert.IsTrue(File.Exists(expectedFile), "File wasn't copied from imports directory.");


			// Add another file
			var script2File = sourceDir
				+ Path.DirectorySeparatorChar
				+ "scripts"
					+ Path.DirectorySeparatorChar
					+ "TestScript2.cs";

			var script2Content = "Test content 2";

			Console.WriteLine ("Creating a second test script in project one...");
			Console.WriteLine ("");

			File.WriteAllText(script2File, script2Content);
			
			Console.WriteLine ("Adding the import pattern...");
			Console.WriteLine ("");

			// 
			script.Importer.AddImportPattern("DestinationProject", script.ToRelative(script2File));

			Console.WriteLine ("Syncing the imports...");
			Console.WriteLine ("");

			script.Importer.Sync("DestinationProject", script2File);
			
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

			// Move into destination project
			script.Relocate(project2Dir);
			
			Console.WriteLine ("Pulling git changes...");
			Console.WriteLine ("");
*/
			var expectedFile3Path = destinationDir
				+ Path.DirectorySeparatorChar
					+ "scripts"
					+ Path.DirectorySeparatorChar
					+ Path.GetFileName(script2File);

			Assert.IsTrue(File.Exists(expectedFile3Path), "File wasn't pulled to project two.");
		}
	}
}

