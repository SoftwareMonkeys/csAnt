using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Tests
{
	public class BaseTestFixture
	{
		public BaseTestFixture ()
		{
		}

		public string GetRoot()
		{
			var path = Path.GetDirectoryName(
				Environment.CurrentDirectory
			);

			path = Path.GetDirectoryName(
				path
			);

			return path;
		}
		
		public virtual TestScript GetTestScript()
		{

			var testScript = new TestScript(
				"TestScript",
				this
			);

			InitializeTestScript(testScript);


			return testScript;
		}

		public virtual void InitializeTestScript(ITestScript script)
		{
			Console.WriteLine ("");
			Console.WriteLine ("Initializing test script...");
			Console.WriteLine ("");

			var node = script.CurrentNode;

			script.CurrentDirectory = Path.GetDirectoryName(node.FilePath);

			var originalDir = GetOriginalDirectory(script);

			var tmpDir = script.GetTmpDir();

			script.CurrentDirectory = tmpDir;
			
			Console.WriteLine("Current directory:");
			Console.WriteLine(script.CurrentDirectory);
			Console.WriteLine("");

			script.IsVerbose = true;

			script.OriginalDirectory = originalDir;
			
			Console.WriteLine("Original directory:");
			Console.WriteLine(script.OriginalDirectory);
			Console.WriteLine("");

			// TODO: Remove if not needed
			//testScript.CopyTestFiles(actualProjectDir, tmpDir);


		}

		public string GetOriginalDirectory(ITestScript script)
		{
			return Path.GetDirectoryName(script.CurrentNode.FilePath);
			// TODO: Clean up
			//ProjectDirectory = Path.GetFullPath("../../../../");
			//return Path.GetFullPath("../../");
		}

		// TODO: Check if this is needed. Should probably use GrabOriginalFiles instead.
		public void CopyTestFiles (ITestScript script)
		{
			var originalDirectory = script.OriginalDirectory;

			var currentDirectory = script.CurrentDirectory;

			Console.WriteLine("Copying test files...");

			Console.WriteLine ("Original directory: " + originalDirectory);
			Console.WriteLine ("Current directory: " + currentDirectory);

			var patterns = new string[]{
				"/lib/**",
				"/src/**"
			};

			foreach (var file in script.FindFiles (originalDirectory, patterns))
			{
				var shortFileName = file.Replace(originalDirectory, "");

				var destinationFileName = currentDirectory
					+ Path.DirectorySeparatorChar
						+ shortFileName;

				script.EnsureDirectoryExists(Path.GetDirectoryName(destinationFileName));

				Console.WriteLine ("From:");
				Console.WriteLine (file);
				Console.WriteLine ("To:");
				Console.WriteLine (destinationFileName);

				File.Copy(file, destinationFileName);
			}
		}
	}
}

