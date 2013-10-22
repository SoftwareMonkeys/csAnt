using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Tests
{
	public class TestUtilities
	{
		public ITestScript Script { get;set; }

		public TestUtilities (
			ITestScript script
		)
		{
			Script = script;
		}

		public void CopyTestResults (string fromProjectDirectory, string toProjectDirectory)
		{
			var fromTestsDir = fromProjectDirectory
				+ Path.DirectorySeparatorChar
				+ "tests"
				+ Path.DirectorySeparatorChar
				+ "results";

			var destinationTestsDir = toProjectDirectory
				+ Path.DirectorySeparatorChar
				+ "tests"
				+ Path.DirectorySeparatorChar
				+ "results";

			var groupName = GetGroupName ();

			Console.WriteLine ("Group name: " + groupName);
			
			Console.WriteLine("Moving test results from:");
			Console.WriteLine("  " + fromTestsDir);
			Console.WriteLine("To:");
			Console.WriteLine("  " + destinationTestsDir);
			Console.WriteLine("");

			foreach (var typeDir in Directory.GetDirectories(fromTestsDir)) {
				foreach (var dateDir in Directory.GetDirectories(typeDir))
				{
					foreach (var formatDir in Directory.GetDirectories(dateDir))
					{
						var type = Path.GetFileNameWithoutExtension(typeDir);

						var date = Path.GetFileNameWithoutExtension(dateDir);

						var format = Path.GetFileNameWithoutExtension(formatDir);

						var toDir = destinationTestsDir
							+ Path.DirectorySeparatorChar
							+ type
							+ Path.DirectorySeparatorChar
							+ date
							+ Path.DirectorySeparatorChar
							+ format;
						
						if (!String.IsNullOrEmpty (groupName)) {
							toDir = toDir
								+ Path.DirectorySeparatorChar
								+ groupName;
						}

						Script.CopyDirectory(formatDir, toDir);
					}
				}
			}

			//Script.CopyDirectory(fromTestsDir, destinationTestsDir);
		}

		public string GetGroupName()
		{
			IScript s = Script;

			string groupName = String.Empty;

			while (s != null)
			{
				if (s is ITestScript)
					groupName = ((ITestScript)s).TestGroupName;

				if (s.Console is SubConsoleWriter)
					s = ((SubConsoleWriter)s.Console).ParentWriter.Script;
				else
					s = null;
			}

			return groupName;
		}
	}
}

