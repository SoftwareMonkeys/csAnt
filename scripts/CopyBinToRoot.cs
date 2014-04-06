//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Projects.dll

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
        int s = 0;

		var patterns = new string[]{
			"bin/Release/packed/csAnt-SetUpFromLocal.*",
			"bin/Release/packed/csAnt-SetUp.*"
		};

	        foreach (string file in FindFiles(patterns))
	        {
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

                var isNewer = File.GetLastWriteTime(file) > File.GetLastWriteTime(toFile);

                if (!File.Exists(toFile) || isNewer)
                {
    		        File.Copy(file, toFile, true);

    		        i++;
                }
                else
                    s++;
	        }

		Console.WriteLine(i + " files copied.");
		Console.WriteLine(s + " files skipped.");

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
