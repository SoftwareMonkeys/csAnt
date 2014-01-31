//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;

using System;
using System.IO;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class CopyPackageToRepositoryScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new CopyPackageToRepositoryScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		string pkgsDirectory = ProjectDirectory
			+ Path.DirectorySeparatorChar
			+ "pkgs";
	
	        Console.WriteLine("");
	        Console.WriteLine("Packages directory:");
	        Console.WriteLine(pkgsDirectory);
	        Console.WriteLine("");

		int i = 0;

                // TODO: Figure out a centralized way of generating the path
		var repoDir = Path.GetFullPath(
			OriginalDirectory
			+ "/../../pkgs"		
		);

		Console.WriteLine("Repo dir:");
		Console.WriteLine(repoDir);
	        Console.WriteLine("");

                foreach (string dir in Directory.GetDirectories(pkgsDirectory))
                {
                        var packageName = Path.GetFileName(dir);
                
			var zipDir = dir
				+ Path.DirectorySeparatorChar
				+ "zip";

		        foreach (string file in Directory.GetFiles(zipDir))
		        {
			        i++;

			        string toFile = file.Replace(
					zipDir,
					repoDir + Path.DirectorySeparatorChar + packageName
				);
		
			        if (!Directory.Exists(Path.GetDirectoryName(toFile)))
				        Directory.CreateDirectory(Path.GetDirectoryName(toFile));

			        Console.WriteLine("Copying: "
				        + file.Replace(ProjectDirectory, "")
			        );

			        Console.WriteLine("To: "
				        + toFile
			        );

				EnsureDirectoryExists(Path.GetDirectoryName(toFile));

				if (!File.Exists(toFile))
			        	File.Copy(file, toFile, true);
		        }
		}

		Console.WriteLine(i + " files copied.");

		AddSummary("Moved " + i + " files from '/bin/' to '" + repoDir + "'");

		return true;
	}

}
