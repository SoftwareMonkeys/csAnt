using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public virtual string GetTemporaryDirectory()
		{
			return GetTmpDir();
		}

		public virtual string GetTmpDir()
		{
			if (IsVerbose) {
				Console.WriteLine ("");
				Console.WriteLine ("Getting temporary directory...");
			}

			var root = GetTmpRoot ();

			var name = Path.GetFileName(CurrentDirectory);

			var path = root
				+ Path.DirectorySeparatorChar
				+ GetTimeStamp()
				+ Path.DirectorySeparatorChar
				+ name;

			if (IsVerbose) {
				Console.WriteLine ("Root:");
				Console.WriteLine (root);
				Console.WriteLine ("Path:");
				Console.WriteLine (path);
				Console.WriteLine ("");
			}

			EnsureDirectoryExists(path);

			return path;
		}
	}
}

