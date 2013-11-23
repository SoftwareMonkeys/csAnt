using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void CopyDirectory (string source, string destination)
        {
            CopyDirectory(source, destination, false);
        }

        public void CopyDirectory (string source, string destination, bool overwrite)
        {
            if (source == destination)
                throw new ArgumentException("Cannot copy. The source and destination paths are both: " + source);

            if (IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Copying directory:");
                Console.WriteLine (source);
                Console.WriteLine ("To:");
                Console.WriteLine (destination);
                Console.WriteLine ("");
            }

			// Create all of the directories
			foreach (string dirPath in Directory.GetDirectories(source, "*", 
			    SearchOption.AllDirectories))
				Directory.CreateDirectory (dirPath.Replace (source, destination));

			// Copy all the files
			foreach (string newPath in Directory.GetFiles(source, "*.*", SearchOption.AllDirectories))
			{
				var destinationFile = newPath.Replace (source, destination);

				EnsureDirectoryExists(Path.GetDirectoryName(destinationFile));

                if (IsVerbose)
                {
                    Console.WriteLine ("Copying: " + newPath);
                    Console.WriteLine ("To: " + destinationFile);
                }

                File.Copy (newPath, destinationFile, overwrite);
			}
		}
	}
}

