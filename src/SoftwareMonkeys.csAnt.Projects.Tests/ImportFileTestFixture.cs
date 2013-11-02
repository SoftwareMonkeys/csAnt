using System;
using NUnit.Framework;
using System.IO;

namespace SoftwareMonkeys.csAnt.Projects.Tests
{
	[TestFixture]
	public class ImportFileTestFixture : BaseProjectsTestFixture
	{
		[Test]
		public void Test_ImportFile()
		{
			var script = GetTestScript();

			script.IsVerbose = true;

			var dir = script.CurrentDirectory;

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

			script.EnsureDirectoryExists(project1Dir);
			script.EnsureDirectoryExists(project2Dir);

			// Move to second project
			script.Relocate(project2Dir);

			// Initialize git
			script.GitInit();

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
			script.GitAdd (scriptFile);

			// Git commit
			script.GitCommit();

			// Switch back to project one
			script.Relocate(project1Dir);

			// Add project 2 as import
			script.AddImport("ProjectTwo", project2Dir);

			script.ImportFile("ProjectTwo", "scripts/TestScript.cs", "scripts", false);

			var expectedFile = project1Dir
				+ Path.DirectorySeparatorChar
				+ "scripts"
				+ Path.DirectorySeparatorChar
					+ "TestScript.cs";

			Assert.IsTrue(File.Exists(expectedFile), "File wasn't copied.");
		}
	}
}

