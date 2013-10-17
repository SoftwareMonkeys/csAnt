using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Projects
{
	public partial class BaseProjectScript
	{
		public void CopyTestResults(string fromProjectDirectory, string toProjectDirectory)
		{
			var fromTestsDir = fromProjectDirectory
				+ Path.DirectorySeparatorChar
				+ "tests";

			var destinationTestsDir = toProjectDirectory
				+ Path.DirectorySeparatorChar
				+ "tests";

			Console.WriteLine("Moving test results from:");
			Console.WriteLine("  " + fromTestsDir);
			Console.WriteLine("To:");
			Console.WriteLine("  " + destinationTestsDir);

			CopyDirectory(fromTestsDir, destinationTestsDir);
		}
	}
}

