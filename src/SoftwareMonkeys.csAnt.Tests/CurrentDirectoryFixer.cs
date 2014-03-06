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
            	
                if (currentDirectory.EndsWith (key))
                    currentDirectory = currentDirectory.Replace (key, "");
                
                foreach (var target in targets)
                {
            		var subKey = Path.DirectorySeparatorChar
	            		+ "bin"
	            		+ Path.DirectorySeparatorChar
	            		+ mode
	            		+ Path.DirectorySeparatorChar
	            		+ target;
            	
	                if (currentDirectory.EndsWith (subKey))
	                    currentDirectory = currentDirectory.Replace (subKey, "");
                }
            }
            
            return currentDirectory;
		}
	}
}
