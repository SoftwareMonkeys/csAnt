using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Tests
{
	/// <summary>
	/// Fixes the current working directory from the assembly execution directory to the test directory.
	/// </summary>
	public class CurrentDirectoryFixer
	{
		public CurrentDirectoryFixer()
		{
		}
		
		public string Fix(string currentDirectory)
		{
			currentDirectory = currentDirectory.TrimEnd(Path.DirectorySeparatorChar);
			
            var modes = new string[]{"Debug", "Release"};
            
            var targets = new string[]{"x86"};

            foreach (var mode in modes) {
                var key = Path.DirectorySeparatorChar
                    + "bin"
                    + Path.DirectorySeparatorChar
                    + mode;

                var key2 = Path.DirectorySeparatorChar
                    + "bin-tests"
                    + Path.DirectorySeparatorChar
                    + mode;
            	
                if (currentDirectory.EndsWith (key))
                {
                    currentDirectory = currentDirectory.Replace (key, "");
                    break;
                }

                if (currentDirectory.EndsWith (key2))
                {
                    currentDirectory = currentDirectory.Replace (key2, "");
                    break;
                }
                
                foreach (var target in targets)
                {
                    var subKey = Path.DirectorySeparatorChar
                        + "bin"
                        + Path.DirectorySeparatorChar
                        + target
                        + Path.DirectorySeparatorChar
                        + mode;

                    var subKey2 = Path.DirectorySeparatorChar
                        + "bin-tests"
                        + Path.DirectorySeparatorChar
                        + target
                        + Path.DirectorySeparatorChar
                        + mode;
            	
                    if (currentDirectory.EndsWith (subKey))
                    {
                        currentDirectory = currentDirectory.Replace (subKey, "");
                        break;
                    }

                    if (currentDirectory.EndsWith (subKey2))
                    {
                        currentDirectory = currentDirectory.Replace (subKey2, "");
                        break;
	                }
                }
            }
            
            return currentDirectory;
		}
	}
}
