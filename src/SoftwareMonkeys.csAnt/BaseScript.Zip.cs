using System;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void Zip(string[] filePatterns, string zipFilePath)
		{
			if (!Directory.Exists(Path.GetDirectoryName(zipFilePath)))
				Directory.CreateDirectory(Path.GetDirectoryName(zipFilePath));

			var zipFileName = Path.GetFileNameWithoutExtension(zipFilePath);

			// Zip up the files - From SharpZipLib Demo Code
           	ZipOutputStream s = new ZipOutputStream(
				File.Create(zipFilePath)
			);

			if (IsVerbose)
				Console.WriteLine ("  Project directory: " + ProjectDirectory);
            
            s.SetLevel(9); // 0-9, 9 being the highest compression

            byte[] buffer = new byte[4096];

            foreach (string pattern in filePatterns)
            {
				if (!String.IsNullOrEmpty(pattern))
				{
					Console.WriteLine ("  Specified pattern/file: " + pattern);

					string shortPattern = pattern.Replace(ProjectDirectory, "");

					string[] foundFiles = FindFiles(ProjectDirectory, shortPattern);
					
					Console.WriteLine ("    Found " + foundFiles.Length.ToString() + " files.");
					
					Console.WriteLine ("    Zipping...");

					foreach (string foundFile in foundFiles)
					{
						Console.WriteLine ("    " + foundFile.Replace (ProjectDirectory, ""));

						var internalPath = zipFileName
							+ "/"
							+ foundFile.Replace(ProjectDirectory, "");

		                ZipEntry entry = new ZipEntry(
							internalPath
						);

		                entry.DateTime = DateTime.Now;
		                s.PutNextEntry(entry);

		                using (FileStream fs = File.OpenRead(foundFile))
		                {
		                    int sourceBytes;
		                    do
		                    {
		                        sourceBytes = fs.Read(buffer, 0,
		                        buffer.Length);

		                       s.Write(buffer, 0, sourceBytes);

		                    } while (sourceBytes > 0);
		                }
					}
	            }
			}

            s.Finish();
            s.Close();

		}
	}
}