using System;
using System.IO;
using System.Collections.Generic;

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
		}

		public string GetOriginalDirectory(ITestScript script)
		{
			return Path.GetDirectoryName(script.CurrentNode.FilePath);
		}
	}
}

