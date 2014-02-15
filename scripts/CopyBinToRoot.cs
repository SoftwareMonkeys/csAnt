//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;

using System;
using System.IO;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class CopyBinToRootScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new CopyBinToRootScript().Start(args);
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

		var patterns = new string[]{
			"bin/Release/Packed/SetUpFromLocal.exe",
			"bin/Release/Packed/csAnt.exe"		
		};

	        foreach (string file in FindFiles(patterns))
	        {
		        i++;

		        string toFile = CurrentDirectory
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

		        File.Copy(file, toFile, true);
	        }

		Console.WriteLine(i + " files copied.");

		AddSummary("Moved " + i + " files from '/bin/*' to '/'");

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
