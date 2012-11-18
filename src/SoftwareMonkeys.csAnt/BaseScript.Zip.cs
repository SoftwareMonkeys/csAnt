using System;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		/// <summary>
		/// Zips the files matching the specified file patterns.
		/// </summary>
		/// <param name='filePatterns'>
		/// An array of patterns used to match the files being zipped.
		/// Example patterns:
		/// /SourceFolder/MyFile.txt - single file
		/// /SourceFolder/* - multiple files with a wildcard
		/// /SourceFolder/*.txt - multiple files of a particular type with a wildcard
		/// /SourceFolder/MyFile.txt|/NewLocation/ - specifies a new location for the file within the zip (wildcards can also be used)
		/// </param>
		/// <param name='zipFilePath'>
		/// 
		/// </param>
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
				Console.WriteLine ("  Current directory: " + CurrentDirectory);
            
            s.SetLevel(9); // 0-9, 9 being the highest compression

            byte[] buffer = new byte[4096];

            foreach (string patternLine in filePatterns)
            {
				if (!String.IsNullOrEmpty(patternLine))
				{
					Console.WriteLine ("  Specified pattern/file: " + patternLine);

					string pattern = patternLine;

					if (patternLine.IndexOf("|") > -1)
					{
						pattern = patternLine.Split('|')[0];
					}

					string shortPattern = pattern.Replace(CurrentDirectory, "");

					string[] foundFiles = FindFiles(CurrentDirectory, shortPattern);
					
					Console.WriteLine ("    Found " + foundFiles.Length.ToString() + " files.");
					
					Console.WriteLine ("    Zipping...");

					foreach (string foundFile in foundFiles)
					{
						Console.WriteLine ("    " + foundFile.Replace (CurrentDirectory, ""));

						var internalPath = GetZipInternalPath(
							zipFileName,
							foundFile,
							patternLine
						);

		                ZipEntry entry = new ZipEntry(
							internalPath
						);

						entry.DateTime = File.GetLastWriteTime(foundFile);

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

		public string GetZipInternalPath(string zipName, string fileName, string originalPattern)
		{
			var subPath = String.Empty;

			if (originalPattern.IndexOf("|") > -1)
			{
				var patternParts = originalPattern.Split('|');

				subPath = patternParts[1]
					+ Path.GetFileName(fileName);
			}
			else
			{
				subPath = fileName.Replace(CurrentDirectory, "");
			}

			return zipName
				+ "/"
				+ subPath.Trim('/');
		}
	}
}