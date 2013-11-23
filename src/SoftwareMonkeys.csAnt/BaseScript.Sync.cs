using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void Sync(string fromDir, string toDir)
		{
            Sync (fromDir, toDir, "**");
		}
		
		public void Sync (string fromDir, string toDir, string pattern)
		{
			foreach (var file in FindFiles (fromDir, pattern)) {
				var toFile = file.Replace(fromDir, toDir);

				if (!File.Exists(toFile))
				{
					Console.WriteLine();
					Console.WriteLine ("File not found in destination. Copying:");
					Console.WriteLine(toFile.Replace(toDir, ""));
					Console.WriteLine();

					File.Copy(file, toFile);
				}
				else if (File.GetLastWriteTime(file) > File.GetLastWriteTime(toFile))
				{
					Console.WriteLine();
					Console.WriteLine ("File is newer than destination. Copying:");
					Console.WriteLine(toFile.Replace(toDir, ""));
					Console.WriteLine();

					BackupFile(toFile);

					File.Copy(file, toFile, true);
				}
			}
		}
	}
}

