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
		public void Zip(string zipFilePath, params string[] filePatterns)
		{
            var cmd = new ZipCommand(
                this,
                zipFilePath,
                filePatterns
                );

            Execute(cmd);
		}
	}
}