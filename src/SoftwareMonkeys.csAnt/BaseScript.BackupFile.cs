using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public string BackupFile(string relativeFilePath)
		{
			var fromFullFilePath = String.Empty;

			fromFullFilePath = Path.GetFullPath(relativeFilePath);

			var toFilePath = CurrentDirectory
				+ Path.DirectorySeparatorChar
				+ "_bak"
				+ Path.DirectorySeparatorChar
				+ relativeFilePath;

			var timeStamp = String.Format(
				"[{0}-{1}-{2}--{3}-{4}-{5}]",
			    DateTime.Now.Year,
				DateTime.Now.Month,
				DateTime.Now.Day,
				DateTime.Now.Hour,
				DateTime.Now.Minute,
				DateTime.Now.Second
			);

			var ext = Path.GetExtension(toFilePath);

			var toFileName = Path.GetFileNameWithoutExtension(toFilePath);

			toFilePath = Path.GetDirectoryName(toFilePath)
				+ Path.DirectorySeparatorChar
				+ toFileName
				+ Path.DirectorySeparatorChar
				+ toFileName
				+ "-" + timeStamp
				+ ext;
			
			if (!Directory.Exists(Path.GetDirectoryName(toFilePath)))
				Directory.CreateDirectory(Path.GetDirectoryName(toFilePath));

			Console.WriteLine("");
			Console.WriteLine("Backing up file:");
			Console.WriteLine("  " + fromFullFilePath.Replace (CurrentDirectory, ""));
			Console.WriteLine("To:");
			Console.WriteLine("  " + toFilePath.Replace (CurrentDirectory, ""));
			Console.WriteLine("");

			File.Copy(
				fromFullFilePath,
				toFilePath
			);

			return toFilePath;
		}
	}
}

