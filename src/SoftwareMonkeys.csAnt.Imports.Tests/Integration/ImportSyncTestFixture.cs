using System;
using System.IO;
using NUnit.Framework;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt.Tests
{
	[TestFixture]
	public class ImportSyncTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_ImportSync()
		{
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

			Console.WriteLine ("Creating test project directories...");
			Console.WriteLine ("");

			script.EnsureDirectoryExists(project1Dir);
			script.EnsureDirectoryExists(project2Dir);
			
			Console.WriteLine ("Relocating to project 2...");
			Console.WriteLine ("");

			// Move to second project
			script.Relocate(project2Dir);
            
            // Create the required file nodes
            script.CreateNodes();
			
			Console.WriteLine ("Initializing git repository...");
			Console.WriteLine ("");

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
			
			Console.WriteLine ("Relocating to project 1...");
			Console.WriteLine ("");

			// Switch back to project one
			script.Relocate(project1Dir);

            // Create the required file nodes
            script.CreateNodes();
			
			Console.WriteLine ("Adding project 2 as an import project...");
			Console.WriteLine ("");

			// Add project 2 as import
			script.Importer.AddImport("ProjectTwo", project2Dir);

			Console.WriteLine ("Importing test script to project from project two...");
			Console.WriteLine ("");

			script.Importer.ImportFile("ProjectTwo", "scripts/TestScript.cs", "scripts", false);

			var expectedFile = project1Dir
				+ Path.DirectorySeparatorChar
				+ "scripts"
				+ Path.DirectorySeparatorChar
					+ "TestScript.cs";

			
			Console.WriteLine ("Checking that the script was imported...");
			Console.WriteLine ("");

			Assert.IsTrue(File.Exists(expectedFile), "File wasn't copied from imports directory.");


			// Add another file
			var script2File = project1Dir
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
			script.Importer.AddImportPattern("ProjectTwo", script.ToRelative(script2File));

			Console.WriteLine ("Syncing the imports...");
			Console.WriteLine ("");

			script.Importer.Sync("ProjectTwo", script2File);
			
			var expectedFile2 = script.Importer.StagingDirectory
				+ Path.DirectorySeparatorChar
				+ "ProjectTwo"
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
			var expectedFile3Path = project2Dir
				+ Path.DirectorySeparatorChar
					+ "scripts"
					+ Path.DirectorySeparatorChar
					+ Path.GetFileName(script2File);

			Assert.IsTrue(File.Exists(expectedFile3Path), "File wasn't pulled to project two.");
		}
	}
}

