using System;
using System.IO;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class CopyBinToLibScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new CopyBinToLibScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		string binDirectory = ProjectDirectory
			+ Path.DirectorySeparatorChar
			+ "bin";
	
	        Console.WriteLine("");
	        Console.WriteLine("Bin directory:");
	        Console.WriteLine(binDirectory);
	        Console.WriteLine("");

		int i = 0;

                foreach (string dir in Directory.GetDirectories(binDirectory))
                {
                        var mode = Path.GetFileName(dir);
                
		        foreach (string file in Directory.GetFiles(dir))
		        {
			        i++;

			        string toFile = GetLibDir()
				        + Path.DirectorySeparatorChar
				        + mode
				        + Path.DirectorySeparatorChar
				        + Path.GetFileName(file);
		
			        if (!Directory.Exists(Path.GetDirectoryName(toFile)))
				        Directory.CreateDirectory(Path.GetDirectoryName(toFile));

			        Console.WriteLine("Copying: "
				        + file.Replace(ProjectDirectory, "")
			        );

			        Console.WriteLine("To: "
				        + toFile.Replace(ProjectDirectory, "")
			        );

				try
				{
			        	File.Copy(file, toFile, true);
				}
				catch (Exception)
				{
					Console.WriteLine("Can't copy: " + file);
				}
		        }
		}

		Console.WriteLine(i + " files copied.");

		AddSummary("Moved " + i + " files from '/bin/' to '/lib/" + ProjectName + "'");

		return true;
	}

	public string GetLibDir()
	{
		return ProjectDirectory
			+ Path.DirectorySeparatorChar
			+ "lib"
			+ Path.DirectorySeparatorChar
			+ ProjectName
			+ Path.DirectorySeparatorChar
			+ "bin";
	}
}
