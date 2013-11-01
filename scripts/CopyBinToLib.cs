//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;

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
	
	public override bool Start(string[] args)
	{
		string binDirectory = ProjectDirectory
			+ Path.DirectorySeparatorChar
			+ "bin"
			+ Path.DirectorySeparatorChar
			+ "Release";

		int i = 0;

		foreach (string file in Directory.GetFiles(binDirectory))
		{
			i++;

			string toFile = GetLibDir()
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

			RenameExisting(toFile);

			File.Copy(file, toFile, true);
		}

		ClearBackups();

		Console.WriteLine(i + " files copied.");

		AddSummary("Moved " + i + " files from '/bin/' to '/lib/" + ProjectName + "'");

		return true;
	}

	public void RenameExisting(string file)
	{
		if (File.Exists(file))
		{
			var toFile = file + ".bak";		

			if (File.Exists(toFile))
				File.Delete(toFile);

			File.Move(file, toFile);
		}
	}

	public void ClearBackups()
	{
		var dir = GetLibDir();

		foreach (string file in Directory.GetFiles(dir, "*.bak"))
		{
			File.Delete(file);
		}
	}

	public string GetLibDir()
	{
		return ProjectDirectory
			+ Path.DirectorySeparatorChar
			+ "lib"
			+ Path.DirectorySeparatorChar
			+ ProjectName
			+ Path.DirectorySeparatorChar
			+ "bin"
			+ Path.DirectorySeparatorChar
			+ "Release";
	}
}
