using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void CopyTestFiles (string sourceDirectory, string destinationDirectory)
		{
			Console.WriteLine("Copying test files...");

			Console.WriteLine ("Source directory: " + sourceDirectory);

			var patterns = new string[]{
				"/lib/**",
				"/src/**"
			};

			foreach (var file in FindFiles (sourceDirectory, patterns))
			{
				var shortFileName = file.Replace(sourceDirectory, "");

				var destinationFileName = destinationDirectory
					+ Path.DirectorySeparatorChar
						+ shortFileName;

				EnsureDirectoryExists(Path.GetDirectoryName(destinationFileName));

				File.Copy(file, destinationFileName);
			}
		}
	}
}

